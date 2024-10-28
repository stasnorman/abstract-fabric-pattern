# Абстрактная фабрика (шаблон проектирования)

Проект **Абстрактная фабрика** создан с использованием паттерна проектирования "Абстрактная фабрика". Основная задача — абстрагирование от конкретной реализации базы данных и динамическое переключение между источниками данных (SQL Server и PostgreSQL) через абстрактные фабрики.

### Структура проекта

Проект состоит из следующих ключевых компонентов:

- **Controllers**
  - `UserController.cs` — контроллер, который позволяет выполнять операции получения данных о пользователях из обеих баз данных, используя фабрики для управления подключениями.

- **DatabaseContexts**
  - `SqlServerDbContext.cs` — предоставляет контекст базы данных для SQL Server, включая настройки подключения и привязку сущностей.
  - `PostgreSqlDbContext.cs` — предоставляет контекст базы данных для PostgreSQL, реализует аналогичную функциональность с учетом особенностей PostgreSQL.

- **Factories**
  - `IDatabaseFactory.cs` — интерфейс, определяющий контракт для фабрик баз данных, требующий реализации метода `GetContext` для получения контекста базы данных.
  - `SqlServerFactory.cs` — конкретная фабрика для SQL Server, возвращает экземпляр `SqlServerDbContext`.
  - `PostgreSqlFactory.cs` — конкретная фабрика для PostgreSQL, возвращает экземпляр `PostgreSqlDbContext`.

- **Models**
  - `User.cs` — модель, представляющая сущность пользователя в системе, включает основные свойства, такие как `Id`, `Name` и `Email`.

- **appsettings.json** — содержит строки подключения для SQL Server и PostgreSQL, используемые контекстами баз данных.

- **Program.cs** — точка входа в приложение, где происходит регистрация зависимостей, включая фабрики и резолвер, который динамически выбирает нужную фабрику по имени.

### Описание работы

Проект реализует паттерн "Абстрактная фабрика" для создания различных контекстов баз данных через резолвер в зависимости от имени базы данных. Это позволяет модульно и гибко управлять подключениями к различным источникам данных без необходимости модификации контроллера. Для выбора конкретной фабрики используется `Func<string, IDatabaseFactory>`, которая в `Program.cs` регистрируется с поддержкой `SqlServerFactory` и `PostgreSqlFactory`.

### Дополнительная информация

Для более подробного понимания паттерна "Абстрактная фабрика" и его применения в программировании рекомендуется ознакомиться с [вики-страницей о паттерне](https://ru.wikipedia.org/wiki/%D0%90%D0%B1%D1%81%D1%82%D1%80%D0%B0%D0%BA%D1%82%D0%BD%D0%B0%D1%8F_%D1%84%D0%B0%D0%B1%D1%80%D0%B8%D0%BA%D0%B0_(%D1%88%D0%B0%D0%B1%D0%BB%D0%BE%D0%BD_%D0%BF%D1%80%D0%BE%D0%B5%D0%BA%D1%82%D0%B8%D1%80%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D1%8F)).

Этот проект подходит для расширения на несколько типов баз данных с минимальными изменениями, так как добавление нового типа базы данных потребует создания только нового контекста и фабрики, реализующей `IDatabaseFactory`.