SELECT b.Title, b.ISBN, b.Price, b.WebSite, a.Name as [A Name], r.ReviewText, r.DateAdded
FROM Books b
	JOIN BooksToAuthors ba ON b.Id=ba.BookId
	JOIN Authors a ON ba.AuthorId=a.Id
	Left JOIN Reviews r ON b.Id=r.BookId

SELECT b.Title, COUNT(*)
FROM Books b JOIN Reviews r ON b.Id=r.BookId
GROUP BY b.Title

SELECT ReviewText, DateAdded, b.Title, a.Name
FROM Reviews r
	JOIN Books b ON r.BookId=b.Id
	LEFT JOIN Authors a ON r.AuthorId=a.Id
ORDER BY r.DateAdded
 
SELECT *
FROM Books
