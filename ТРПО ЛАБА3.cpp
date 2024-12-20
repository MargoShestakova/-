// ТРПО ЛАБА3.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
#include <string>
#include <vector>
#include <map>

// Класс для хранения данных о пользователе
class User {
public:
	int id;
	std::string username;
	std::string password; // В реальном проекте хэшируйте пароли!
	bool isLoggedIn; // Флаг, показывающий, авторизован ли пользователь

	// Конструктор класса User
	User(int id, std::string username, std::string password) :
		id(id), username(username), password(password), isLoggedIn(false) {}

	// Метод для проверки пароля
	bool authenticate(const std::string& enteredPassword) const {
		// Проверка пароля (в реальном проекте используйте хэши!)
		return password == enteredPassword;
	}
};

// Класс для хранения данных о книге
class Book {
public:
	int id;
	std::string title;
	std::string author;

	// Конструктор класса Book
	Book(int id, std::string title, std::string author) :
		id(id), title(title), author(author) {}
};

// Класс для хранения данных о комментарии
class Comment {
public:
	int id;
	int userId;
	int bookId;
	std::string text;

	// Конструктор класса Comment
	Comment(int id, int userId, int bookId, std::string text) :
		id(id), userId(userId), bookId(bookId), text(text) {}
};

// Класс для управления пользователями
class UserManager {
private:
	std::map<int, User> users; // Храним пользователей в словаре (ключ - id)

public:
	// Метод для добавления пользователя
	void addUser(const User& user) {
		users[user.id] = user;
	}

	// Метод для аутентификации пользователя
	User* authenticate(int userId, const std::string& password) {
		auto it = users.find(userId); // Поиск пользователя по id
		if (it != users.end()) {  // Проверяем, найден ли пользователь
			if (it->second.authenticate(password)) { // Проверяем пароль
				it->second.isLoggedIn = true;  // Устанавливаем флаг авторизации
				return &(it->second);  // Возвращаем указатель на пользователя
			}
		}
		return nullptr; // Если пользователь не найден или пароль не верен
	}

	// Метод для проверки, является ли пользователь авторизованным
	bool isUserLoggedIn(int userId) const {
		auto it = users.find(userId);  // Поиск пользователя по id
		if (it != users.end()) {  // Проверяем, найден ли пользователь
			return it->second.isLoggedIn;  // Возвращаем флаг авторизации
		}
		return false;  // Если пользователь не найден - он не авторизован
	}
};

// Класс для управления книгами
class BookManager {
private:
	std::vector<Book> books;  // Храним книги в векторе

public:
	// Метод для добавления книги
	void addBook(const Book& book) {
		books.push_back(book);
	}

	// Метод для получения всех книг
	std::vector<Book> getBooks() const {
		return books;
	}

	// Метод для получения книги по id
	Book* getBookById(int id) const {
		for (const auto& book : books) { // Проходим по всем книгам
			if (book.id == id) {
				return new Book(book.id, book.title, book.author);  // Возвращаем копию книги
			}
		}
		return nullptr;  // Если книга не найдена
	}
};

// Класс для управления комментариями
class CommentManager {
private:
	std::map<int, Comment> comments; // Храним комментарии в словаре (ключ - id)

public:
	// Метод для добавления комментария
	void addComment(const Comment& comment) {
		comments[comment.id] = comment;
	}

	// Метод для получения комментариев к книге
	std::vector<Comment> getCommentsForBook(int bookId) const {
		std::vector<Comment> bookComments; // Вектор для хранения комментариев
		for (const auto& comment : comments) {  // Проходим по всем комментариям
			if (comment.second.bookId == bookId) { // Проверяем, относится ли комментарий к нужной книге
				bookComments.push_back(comment.second); // Добавляем в результирующий вектор
			}
		}
		return bookComments; // Возвращаем вектор с комментариями
	}
};

int main() {
	// Создаем объекты менеджеров
	UserManager userManager;
	BookManager bookManager;
	CommentManager commentManager;

	// Добавляем пользователей
	User user1(1, "Alice", "12345");
	User user2(2, "Bob", "67890");
	userManager.addUser(user1);
	userManager.addUser(user2);

	// Добавляем книги
	Book book1(1, "The Lord of the Rings", "J.R.R. Tolkien");
	Book book2(2, "Pride and Prejudice", "Jane Austen");
	bookManager.addBook(book1);
	bookManager.addBook(book2);

	
		// Аутентификация пользователя
		User* authenticatedUser = userManager.authenticate(1, "12345"); // Пытаемся авторизовать Alice
	if (authenticatedUser != nullptr) { // Если пользователь найден и пароль верен
		std::cout << "Authentication successful for user: " << authenticatedUser->username << std::endl;
	}
	else { // Если пользователь не найден или пароль не верен
		std::cout << "Authentication failed." << std::endl;
	}

	// Добавляем комментарий к книге
	Comment comment(1, 1, 1, "Great book!");
	commentManager.addComment(comment);

	return 0;
}

// Запуск программы: CTRL+F5 или меню "Отладка" > "Запуск без отладки"
// Отладка программы: F5 или меню "Отладка" > "Запустить отладку"

// Советы по началу работы 
//   1. В окне обозревателя решений можно добавлять файлы и управлять ими.
//   2. В окне Team Explorer можно подключиться к системе управления версиями.
//   3. В окне "Выходные данные" можно просматривать выходные данные сборки и другие сообщения.
//   4. В окне "Список ошибок" можно просматривать ошибки.
//   5. Последовательно выберите пункты меню "Проект" > "Добавить новый элемент", чтобы создать файлы кода, или "Проект" > "Добавить существующий элемент", чтобы добавить в проект существующие файлы кода.
//   6. Чтобы снова открыть этот проект позже, выберите пункты меню "Файл" > "Открыть" > "Проект" и выберите SLN-файл.
