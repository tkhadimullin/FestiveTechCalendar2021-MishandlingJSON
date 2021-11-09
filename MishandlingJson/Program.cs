using Microsoft.EntityFrameworkCore;
using MishandlingJson;
using MishandlingJson.Models;
using Newtonsoft.Json;

var context = new SHDbContext();
//var sql = context.WishlistEntries.Select(wle => JsonConvert.DeserializeObject<ItemData>(wle.ItemData)
//                                                .Category.Any(c => c.Name == "Connected Home & Housewares")) //The LINQ expression 'c => c.Name == "Connected Home & Housewares"' could not be translated. 
//    .ToQueryString();

// since we can't filter it on server we'll filter on client
//var data = context.WishlistEntries.Select(wle => JsonConvert.DeserializeObject<ItemData>(wle.ItemData))
//                                .AsEnumerable()
//                                .Where(d => d.Category.Any(c => c.Name == "Connected Home & Housewares"))
//                                .ToList();

// how about writing a value converter?
//var data = context.WishlistEntries.Where(d => d.ItemData.Category.Any(c => c.Name == "Connected Home & Housewares")) //The LINQ expression 'DbSet<WishlistEntry>().Where(w => w.ItemData.Category.Any(c => c.Name == "Connected Home & Housewares"))' could not be translated
//                                 .ToList();

//var data = context.WishlistEntries.AsEnumerable().Where(d => d.ItemData.Category.Any(c => c.Name == "Connected Home & Housewares")).ToList();
// this generates a lot of debug noise, so add .Take(1) to see the SQL
// also not ideal as we still have to filter on client side, effectively scanning all table

//how about JSON_VALUE as a filter?
//var data = context.WishlistEntries.Where(d => SHDbContext.JsonValue(d.ItemData, "$.category[0].name").Contains("Connected Home & Housewares")).ToList();

// capture query, show plan
// try to add index CREATE INDEX IX_ItemData ON dbo.[WishlistEntries] (ItemData); - column is VARCHAR(MAX) so index would not get added
// what if we limit the length to 4000? 1700? 850?

// now, use computed column for querying
var data = context.WishlistEntries.Where(wle => wle.ItemCategory == "Connected Home & Housewares").Take(1).ToList();

//how about querying JSON arrays? this is where we ultimately get stuck
/*
var data = context.WishlistEntries.FromSqlRaw(@"SELECT TOP 1 wishlist.*
      FROM [WishlistEntries] as wishlist
CROSS APPLY OPENJSON(ItemData, '$.category') WITH (CatName VARCHAR(200) '$.name') as categories
WHERE categories.CatName = {0}", "Connected Home & Housewares").Take(1).ToList();
*/
