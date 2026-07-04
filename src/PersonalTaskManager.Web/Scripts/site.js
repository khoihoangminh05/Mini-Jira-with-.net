/**
 * Site-wide — Phase 9 dark mode toggle (Bootstrap 5.3 data-bs-theme).
 */
(function () {
    'use strict';

    var STORAGE_KEY = 'ptm-theme';

    function getPreferredTheme() {
        var stored = localStorage.getItem(STORAGE_KEY);
        if (stored === 'dark' || stored === 'light') {
            return stored;
        }
        return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
    }

    function applyTheme(theme) {
        document.documentElement.setAttribute('data-bs-theme', theme);
        var btn = document.getElementById('themeToggleBtn');
        if (btn) {
            var isDark = theme === 'dark';
            btn.setAttribute('aria-pressed', isDark ? 'true' : 'false');
            btn.title = isDark ? 'Chuyển sáng' : 'Chuyển tối';
            btn.innerHTML = isDark ? '☀️' : '🌙';
        }
    }

    function toggleTheme() {
        var current = document.documentElement.getAttribute('data-bs-theme') || 'light';
        var next = current === 'dark' ? 'light' : 'dark';
        localStorage.setItem(STORAGE_KEY, next);
        applyTheme(next);
    }

    document.addEventListener('DOMContentLoaded', function () {
        applyTheme(getPreferredTheme());
        var btn = document.getElementById('themeToggleBtn');
        if (btn) {
            btn.addEventListener('click', toggleTheme);
        }
    });
})();
