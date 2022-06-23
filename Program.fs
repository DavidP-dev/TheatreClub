// For more information see https://aka.ms/fsharp-console-apps
module Program

open System
open Domain
open Business

// Lower is function test

let david = {
    Name = "David"
    Surname = "Pícha"
    Email = "picha@sskpk.cz"
    PreferredGenres = [Art; Philosophy; Comedy]
    Id = Guid.NewGuid()
}
let romana = {
    Name = "Romana"
    Surname = "Kopecká"
    Email = "romcakopca@seznam.cz"
    PreferredGenres = [Art; Philosophy; Dance]
    Id = Guid.NewGuid()
}

let encyklopedie = {
    Id = Guid.NewGuid()
    Title = "Encyklopedia akčního filmu"
    Theatre = "Divadlo pod Palmovkou"
    DateAndTime = DateTimeOffset.Now
    Cost = 350
    Genres = [Comedy ; Philosophy]
}

let conn = Database.getConnection ()

addPerformance conn encyklopedie
