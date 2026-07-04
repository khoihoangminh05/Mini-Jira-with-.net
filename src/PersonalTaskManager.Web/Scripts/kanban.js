/**
 * Kanban — Phase 7 Ajax + Phase 8 SortableJS drag-drop.
 * Quy tắc kéo: được phép thả vào bất kỳ cột nào (To Do → Done trực tiếp OK).
 */
(function ($) {
    'use strict';

    var columnMap = { 1: 'todo', 2: 'inprogress', 3: 'done' };
    var statusMap = { todo: 1, inprogress: 2, done: 3 };
    var sortableInstances = [];

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
            var count = $(this).find('.kanban-column-body .kanban-card:not(.kanban-card-hidden)').length;
            $(this).find('.kanban-column-count').text(count);
            var $body = $(this).find('.kanban-column-body');
            var $empty = $body.find('.kanban-empty-msg');
            var hasVisible = count > 0;
            if (!hasVisible) {
                if ($empty.length === 0) {
                    $body.append('<p class="text-muted small text-center py-3 mb-0 kanban-empty-msg">Không có task</p>');
                }
            } else {
                $empty.remove();
            }
        });
    }

    function prependActivityLog(entry) {
        if (!entry || !entry.message) {
            return;
        }
        var $tl = $('#activityTimeline');
        if (!$tl.length) {
            return;
        }
        $tl.find('.activity-empty-msg').remove();
        var msg = $('<div>').text(entry.message).html();
        var time = entry.timeLabel || '';
        var html = '<li class="list-group-item activity-item">'
            + '<div class="d-flex justify-content-between gap-2">'
            + '<span class="activity-message">' + msg + '</span>'
            + '<time class="text-muted small text-nowrap activity-time">' + time + '</time>'
            + '</div></li>';
        $tl.prepend(html);
    }

    function applyKanbanFilter() {
        var priority = $('#kanbanFilterPriority').val();
        var overdueOnly = $('#kanbanFilterOverdue').is(':checked');

        $('.kanban-card').each(function () {
            var $card = $(this);
            var cardPriority = String($card.data('priority'));
            var isOverdue = $card.data('overdue') === true || $card.data('overdue') === 'true';
            var matchPriority = !priority || cardPriority === priority;
            var matchOverdue = !overdueOnly || isOverdue;
            $card.toggleClass('kanban-card-hidden', !(matchPriority && matchOverdue));
        });
        updateColumnCounts();
    }

    function bindKanbanFilter() {
        $('#kanbanFilterPriority, #kanbanFilterOverdue').on('change', applyKanbanFilter);
        $('#kanbanFilterClear').on('click', function () {
            $('#kanbanFilterPriority').val('');
            $('#kanbanFilterOverdue').prop('checked', false);
            applyKanbanFilter();
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

    function refreshCard($card, taskId) {
        return $.get('/Task/Card', { id: taskId })
            .done(function (html) {
                var $new = $(html);
                $card.replaceWith($new);
                return $new;
            })
            .fail(function () {
                showToast('Không tải lại được card.', 'danger');
            });
    }

    function revertDrag(evt) {
        var item = evt.item;
        var from = evt.from;
        if (evt.from === evt.to) {
            if (evt.oldIndex < evt.newIndex) {
                from.insertBefore(item, from.children[evt.oldIndex]);
            } else {
                from.insertBefore(item, from.children[evt.oldIndex + 1] || null);
            }
        } else {
            from.insertBefore(item, from.children[evt.oldIndex] || null);
        }
        updateColumnCounts();
    }

    function postMove(taskId, projectId, newStatus, newSortOrder, $card, evt) {
        $card.addClass('kanban-card-loading');

        return $.ajax({
            url: '/Task/Move',
            type: 'POST',
            dataType: 'json',
            data: {
                __RequestVerificationToken: getToken(),
                taskId: taskId,
                projectId: projectId,
                newStatus: newStatus,
                newSortOrder: newSortOrder
            }
        })
            .done(function (res) {
                if (res && res.success) {
                    updateColumnCounts();
                    refreshCard($card, taskId);
                    showToast(res.message, 'success');
                    if (res.activityLog) {
                        prependActivityLog(res.activityLog);
                    }
                } else {
                    revertDrag(evt);
                    showToast((res && res.message) || 'Di chuyển thất bại.', 'danger');
                }
            })
            .fail(function (xhr) {
                revertDrag(evt);
                var msg = 'Lỗi máy chủ. Vui lòng thử lại.';
                if (xhr.responseJSON && xhr.responseJSON.message) {
                    msg = xhr.responseJSON.message;
                }
                showToast(msg, 'danger');
            })
            .always(function () {
                $card.removeClass('kanban-card-loading');
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
                        if (res.activityLog) {
                            prependActivityLog(res.activityLog);
                        }
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

    function initSortable() {
        if (typeof Sortable === 'undefined') {
            return;
        }

        sortableInstances.forEach(function (s) { s.destroy(); });
        sortableInstances = [];

        document.querySelectorAll('.kanban-column-body').forEach(function (el) {
            var instance = Sortable.create(el, {
                group: 'kanban',
                animation: 180,
                draggable: '.kanban-card',
                handle: '.kanban-drag-handle',
                ghostClass: 'kanban-sortable-ghost',
                dragClass: 'kanban-sortable-drag',
                delay: 100,
                delayOnTouchOnly: true,
                onEnd: function (evt) {
                    if (evt.from === evt.to && evt.oldIndex === evt.newIndex) {
                        return;
                    }

                    var $item = $(evt.item);
                    var taskId = parseInt($item.data('task-id'), 10);
                    var projectId = parseInt($('.kanban-board').data('project-id'), 10);
                    var columnKey = evt.to.getAttribute('data-column');
                    var newStatus = statusMap[columnKey];
                    var newSortOrder = evt.newIndex;

                    if (!taskId || !projectId || !newStatus) {
                        revertDrag(evt);
                        return;
                    }

                    evt.to.querySelectorAll('.kanban-empty-msg').forEach(function (n) { n.remove(); });
                    updateColumnCounts();
                    postMove(taskId, projectId, newStatus, newSortOrder, $item, evt);
                }
            });
            sortableInstances.push(instance);
        });
    }

    $(function () {
        if ($('.kanban-board').length) {
            bindKanbanAjax();
            bindKanbanFilter();
            initSortable();
        }
    });
})(jQuery);
