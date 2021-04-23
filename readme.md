### Npgsql full text search
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
_context.Books.Where(x => x.SearchVector.Matches(tsQueryString))
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
