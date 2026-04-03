# Avalonia Form Validation

Учебное приложение на Avalonia UI с формой регистрации, валидацией данных и сохранением в JSON файл.

## Функционал

- ✅ Форма с полями: Имя, Фамилия, Отчество, Пол, Возраст 18+
- ✅ Валидация обязательных полей в реальном времени
- ✅ Сохранение данных в JSON файл
- ✅ Автоматическая загрузка данных при запуске
- ✅ Кнопка "Сохранить" активна только при валидных данных

## Технологии

- .NET 9 + C#
- Avalonia UI 11.2.5 — кросс-платформенный фреймворк
- ReactiveUI — MVVM паттерн
- JSON — хранение данных

## Установка и запуск

`bash
git clone https://github.com/Daitbro/AvaloniaFormValidation.git
cd AvaloniaFormValidation
dotnet restore
dotnet run
