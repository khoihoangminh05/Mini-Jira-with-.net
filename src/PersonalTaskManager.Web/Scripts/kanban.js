/**
 * Kanban Ajax — Phase 7: cập nhật trạng thái không reload (fallback form POST khi tắt JS).
 */
(function ($) {
    'use strict';

    var columnMap = { 1: 'todo', 2: 'inprogress', 3: 'done' };

    function getToken() {
        return $('#kanbanAntiForgeryForm input[name="__RequestVerificationToken"]').val()
            || $('input[name="__RequestVerificationToken"]').first().val();
    }

    function showToast(message, type) {
        type = type || 'success';
        var bg = type === 'success' ? 'text-bg-success' : 'text-bg-danger';
        var id = 'toast-' + Date.now();
        var html = '<div id="' + id + '" class="toast align-items-center ' + bg + ' border-0" role="alert" aria-live="assertive" aria-atomic="true">'
            + '<div class="d-flex"><div class="toast-body">' + $('<div>').text(message).html() + '</div>'
            + '<button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button></div></div>';
        var $container = $('#toastContainer');
        $container.append(html);
        var el = document.getElementById(id);
        var toast = bootstrap.Toast.getOrCreateInstance(el, { delay: 3500 });
        toast.show();
        el.addEventListener('hidden.bs.toast', function () { el.remove(); });
    }

    function updateColumnCounts() {
        $('.kanban-column').each(function () {
            var count = $(this).find('.kanban-column-body .kanban-card').length;
            $(this).find('.kanban-column-count').text(count);
            var $empty = $(this).find('.kanban-empty-msg');
            if (count === 0) {
                if ($empty.length === 0) {
                    $(this).find('.kanban-column-body').append('<p class="text-muted small text-center py-3 mb-0 kanban-empty-msg">Không có task</p>');
                }
            } else {
                $empty.remove();
            }
        });
    }

    function moveCardToColumn($card, newStatus) {
        var columnKey = columnMap[newStatus];
        if (!columnKey) return;
        var $target = $('.kanban-column-body[data-column="' + columnKey + '"]');
        $target.find('.kanban-empty-msg').remove();
        $target.append($card);
        $card.attr('data-status', newStatus);
    }

    function refreshCard($card, taskId, callback) {
        $.get('/Task/Card', { id: taskId })
            .done(function (html) {
                var $new = $(html);
                $card.replaceWith($new);
                if (callback) callback($new);
            })
            .fail(function () {
                showToast('Không tải lại được card.', 'danger');
            });
    }

    function bindKanbanAjax() {
        $(document).on('submit', '.kanban-action-form', function (e) {
            e.preventDefault();

            var $form = $(this);
            var $btn = $form.find('button[type="submit"]');
            if ($btn.prop('disabled')) {
                return false;
            }

            var $card = $form.closest('.kanban-card');
            var originalColumn = $card.closest('.kanban-column-body').attr('data-column');
            var action = $form.data('action');
            var taskId = $form.find('input[name="id"]').val();
            var projectId = $form.find('input[name="projectId"]').val();

            $btn.prop('disabled', true);
            $card.addClass('kanban-card-loading');

            $.ajax({
                url: '/Task/UpdateStatus',
                type: 'POST',
                dataType: 'json',
                data: {
                    __RequestVerificationToken: getToken(),
                    id: taskId,
                    projectId: projectId,
                    action: action
                }
            })
                .done(function (res) {
                    if (res && res.success) {
                        moveCardToColumn($card, res.newStatus);
                        updateColumnCounts();
                        refreshCard($card, taskId);
                        showToast(res.message, 'success');
                    } else {
                        showToast((res && res.message) || 'Cập nhật thất bại.', 'danger');
                    }
                })
                .fail(function (xhr) {
                    var msg = 'Lỗi máy chủ. Vui lòng thử lại.';
                    if (xhr.responseJSON && xhr.responseJSON.message) {
                        msg = xhr.responseJSON.message;
                    }
                    showToast(msg, 'danger');
                    // rollback vị trí card nếu đã di chuyển optimistic (chưa di chuyển trước response)
                    var $back = $('.kanban-column-body[data-column="' + originalColumn + '"]');
                    if ($card.parent()[0] !== $back[0]) {
                        $back.find('.kanban-empty-msg').remove();
                        $back.append($card);
                        updateColumnCounts();
                    }
                })
                .always(function () {
                    $btn.prop('disabled', false);
                    $card.removeClass('kanban-card-loading');
                });

            return false;
        });
    }

    $(function () {
        if ($('.kanban-board').length) {
            bindKanbanAjax();
        }
    });
})(jQuery);
