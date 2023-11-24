function toggleButton(button) {
    // Получаем текущий текст кнопки
    var buttonText = button.innerText.trim();

    // Меняем текст кнопки в зависимости от текущего текста
    if (buttonText === "▼") {
        button.innerText = "▲";
    } else {
        button.innerText = "▼";
    }
}

function toggleAllTables() {
    var collapseButtons = document.querySelectorAll('[data-bs-toggle="collapse"]');
    var toggleAllButton = document.getElementById('toggleAllButton');

    var anyTableCollapsed = false;

    collapseButtons.forEach(function (button) {
        var targetId = button.getAttribute('data-bs-target');
        var targetTable = document.querySelector(targetId);

        // Переключение состояния collapse
        var isCollapsed = targetTable.classList.contains('show');

        if (isCollapsed) {
            anyTableCollapsed = true;
        } else {
            targetTable.classList.add('show');
            button.setAttribute('aria-expanded', 'true');
        }
    });

    // Применяем toggleButton к каждой кнопке таблицы
    collapseButtons.forEach(function (button) {
        toggleButton(button);
    });

    // Изменение текста кнопки "Открыть все таблицы" в зависимости от состояния таблиц
    if (anyTableCollapsed) {
        collapseButtons.forEach(function (button) {
            var targetId = button.getAttribute('data-bs-target');
            var targetTable = document.querySelector(targetId);
            targetTable.classList.remove('show');
            button.setAttribute('aria-expanded', 'false');
        });
        toggleAllButton.textContent = 'Открыть все таблицы';
    } else {
        toggleAllButton.textContent = 'Скрыть все таблицы';
    }
}
