// Collections used in queries:
// List<Book> books;
// List<Author> authors;
// List<Loan> loans;
// List<Member> members;


var availableBooks = books.Where(b => b.IsAvailable).ToList();

var titles = books.Select(b => b.Title).ToList();


var programmingBooks = books.Where(b => b.Genre == "Programming").ToList();

var sortedBooks = books.OrderBy(b => b.Title).ToList();

var expensiveBooks = books.Where(b => b.Price > 30).ToList();

var uniqueGenres = books.Select(b => b.Genre).Distinct().ToList();

var countsByGenre = books.GroupBy(b => b.Genre)
                         .Select(g => new { Genre = g.Key, Count = g.Count() });

 var recentBooks = books.Where(b => b.PublishedYear > 2010).ToList();         

    var firstFive = books.Take(5).ToList();

       bool hasExpensive = books.Any(b => b.Price > 50);

             var bookDetails = books.Join(authors, 
                             b => b.AuthorId, 
                             a => a.Id, 
                             (b, a) => new { b.Title, AuthorName = a.Name, b.Genre });

     var avgPrice = books.GroupBy(b => b.Genre)
                    .Select(g => new { Genre = g.Key, AveragePrice = g.Average(b => b.Price) });

                                            
var maxPriceBook = books.OrderByDescending(b => b.Price).FirstOrDefault();

var byDecade = books.GroupBy(b => (b.PublishedYear / 10) * 10)
                    .Select(g => new { Decade = g.Key + "s", Books = g.ToList() });

   var activeMembers = members.Where(m => loans.Any(l => l.MemberId == m.Id && l.ReturnDate == null));

   var frequentBorrowed = loans.GroupBy(l => l.BookId)
                            .Where(g => g.Count() > 1)
                            .Select(g => new { BookId = g.Key, LoanCount = g.Count() });

   var overdue = loans.Where(l => l.DueDate < DateTime.Now && l.ReturnDate == null)
                   .Select(l => l.BookId).ToList();

   var authorCounts = books.GroupBy(b => b.AuthorId)
                        .Select(g => new { AuthorId = g.Key, Count = g.Count() })
                        .OrderByDescending(x => x.Count);

   var priceAnalysis = books.GroupBy(b => b.Price < 20 ? "Cheap" : b.Price <= 40 ? "Medium" : "Expensive")
                         .Select(g => new { Range = g.Key, Count = g.Count() });

   var stats = members.Select(m => new {
    m.Name,
    TotalLoans = loans.Count(l => l.MemberId == m.Id),
    ActiveLoans = loans.Count(l => l.MemberId == m.Id && l.ReturnDate == null),
    AverageDays = loans.Where(l => l.MemberId == m.Id && l.ReturnDate != null)
                       .Select(l => (l.ReturnDate.Value - l.BorrowDate).TotalDays).DefaultIfEmpty(0).Average()
});                                                                                                     