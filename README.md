Book Management System
Описание
Этот проект представляет собой систему управления пользователями, книгами и комментариями. Он включает в себя классы для хранения данных о пользователях, книгах и комментариях, а также менеджеры для управления этими данными.

Функциональность
•  Управление пользователями: Добавление пользователей, аутентификация и проверка статуса авторизации.

•  Управление книгами: Добавление книг, получение списка всех книг и поиск книги по ID.

•  Управление комментариями: Добавление комментариев и получение комментариев для конкретной книги.

Структура проекта
•  User: Класс для хранения данных о пользователе.

•  Book: Класс для хранения данных о книге.

•  Comment: Класс для хранения данных о комментарии.

•  UserManager: Класс для управления пользователями.

•  BookManager: Класс для управления книгами.

•  CommentManager: Класс для управления комментариями.

Использование
1. 
Создание объектов менеджеров:

UserManager userManager;
BookManager bookManager;
CommentManager commentManager;

1. 
Добавление пользователей:

User user1(1, "Alice", "12345");
User user2(2, "Bob", "67890");
userManager.addUser(user1);
userManager.addUser(user2);

1. 
Добавление книг:

Book book1(1, "The Lord of the Rings", "J.R.R. Tolkien");
Book book2(2, "Pride and Prejudice", "Jane Austen");
bookManager.addBook(book1);
bookManager.addBook(book2);

1. 
Аутентификация пользователя:

User* authenticatedUser = userManager.authenticate(1, "12345");
if (authenticatedUser != nullptr) {
std::cout << "Authentication successful for user: " << authenticatedUser->username << std::endl;
} else {
std::cout << "Authentication failed." << std::endl;
}

1. 
Добавление комментария к книге:

Comment comment(1, 1, 1, "Great book!");
commentManager.addComment(comment);

Требования
•  Компилятор C++ (например, GCC, Clang, MSVC)

•  Среда разработки (например, Visual Studio, CLion, Code::Blocks)

Установка и запуск
1. 
Склонируйте репозиторий:

git clone https://github.com/yourusername/book-management-system.git

1. 
Перейдите в директорию проекта:

cd book-management-system

1. 
Скомпилируйте проект:

g++ -o BookManagementSystem main.cpp

1. 
Запустите исполняемый файл:

./BookManagementSystem

Лицензия
Этот проект лицензирован под лицензией MIT. Подробности см. в файле LICENSE.
