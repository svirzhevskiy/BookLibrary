### Npgsql full text search

Use mapping on PostgreSQL 12 and version 5.0.0 of the EF Core provider
```
public class Book //Entities/Book
{
...
    public NpgsqlTsVector SearchVector { get; set; }
}
protected override void OnModelCreating(ModelBuilder builder) //in AppDbContext
{
...
    builder
        .HasGeneratedTsVectorColumn(
            x => x.SearchVector,
            "english",
            x => new {x.Title, x.Blurb})
        .HasIndex(x => x.SearchVector)
        .HasMethod("GIN");
}
```
Or add to Up and Down migration manualy:
Add to Up migration or run on db for creating trigger that creates SearchVector for entity on insert or update
```
migrationBuilder.Sql(
    @"CREATE TRIGGER book_search_vector_update BEFORE INSERT OR UPDATE
    ON ""Books"" FOR EACH ROW EXECUTE PROCEDURE
    tsvector_update_trigger(""SearchVector"", 'pg_catalog.english', ""Title"", ""Blurb"");"
);
```
To Drop migration
```
migrationBuilder.Sql("DROP TRIGGER book_search_vector_update");
```

Using
```
_context.Books.Where(x => x.SearchVector.Matches(tsQueryString)) //BookService
```
For ranking results
```
.Select(x => new
{
...
    Rank = x.SearchVector.Rank(EF.Functions.ToTsQuery("english", tsQueryString))
})
.OrderByDescending(x => x.Rank)
```

[Full text search npgsql]

   [Full text search npgsql]: <https://www.npgsql.org/efcore/mapping/full-text-search.html?tabs=pg12%2Cv5>
