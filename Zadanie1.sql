CREATE TABLE Authors (
    AuthorID INT PRIMARY KEY,
    Name NVARCHAR(255)
);

GO

CREATE TABLE Books (
    BookID INT PRIMARY KEY,
    Title NVARCHAR(255),
    AuthorID INT,
    FOREIGN KEY (AuthorID) REFERENCES Authors(AuthorID)
);

GO

CREATE TABLE Readers (
    ReaderID INT PRIMARY KEY,
    Name NVARCHAR(255)
);

GO

CREATE TABLE BookHistory (
    HistoryID INT PRIMARY KEY,
    BookID INT,
    ReaderID INT,
    CheckoutDate DATE,
    ReturnDate DATE,
    FOREIGN KEY (BookID) REFERENCES Books(BookID),
    FOREIGN KEY (ReaderID) REFERENCES Readers(ReaderID)
);

GO

INSERT INTO Authors (AuthorID, Name) VALUES 
(1, 'Автор1'),
(2, 'Автор2'),
(3, 'Автор3'),
(4, 'Автор4');

GO

INSERT INTO Books (BookID, Title, AuthorID) VALUES 
(1, 'Книга1', 1),
(2, 'Книга2', 1),
(3, 'Книга3', 2),
(4, 'Книга4', 2),
(5, 'Книга5', 2),
(6, 'Книга6', 3),
(7, 'Книга7', 3),
(8, 'Книга8', 4);

GO

INSERT INTO Readers (ReaderID, Name) VALUES 
(1, 'Читатель1'),
(2, 'Читатель2'),
(3, 'Читатель3');

GO

INSERT INTO BookHistory (HistoryID, BookID, ReaderID, CheckoutDate, ReturnDate) VALUES 
(1, 1, 1, '2023-09-10', NULL),
(2, 2, 1, '2023-09-15', '2023-10-01'),
(3, 3, 2, '2023-08-20', NULL),
(4, 4, 2, '2023-09-10', '2023-09-20'),
(5, 5, 3, '2023-09-05', '2023-09-25'),
(6, 6, 3, '2021-12-25', '2022-12-30'),
(7, 7, 1, '2021-12-20', '2023-01-05'),
(8, 8, 2, '2023-07-01', '2023-08-01')

GO

-- читатели, у которых больше 1 книги на руках
SELECT Readers.ReaderID, Readers.Name
FROM Readers
JOIN (
    SELECT ReaderID, COUNT(*) AS BookCount
    FROM BookHistory
    WHERE ReturnDate IS NULL
    GROUP BY ReaderID
) AS ActiveReaders
ON Readers.ReaderID = ActiveReaders.ReaderID
WHERE ActiveReaders.BookCount > 1;


-- все книги, которые сейчас не у читателей
SELECT Books.BookID, Books.Title
FROM Books
LEFT JOIN BookHistory ON Books.BookID = BookHistory.BookID
WHERE BookHistory.ReturnDate IS NOT NULL OR BookHistory.BookID IS NULL;

-- книга у читателя на момент 01.01.2022
SELECT DISTINCT Books.BookID, Books.Title
FROM Books
JOIN BookHistory ON Books.BookID = BookHistory.BookID
WHERE '2022-01-01' BETWEEN BookHistory.CheckoutDate AND BookHistory.ReturnDate;