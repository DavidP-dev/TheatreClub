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
let encyklopedia_romana = {
        ReservationID = Guid.NewGuid()
        MemberId = romana.Id
        PerformanceId  = Guid.NewGuid()
        IsPaid = true
        TicketsReceived = true
        }


[<EntryPoint>]
let main args =
    let conn = Database.getConnection ()
    printfn "Arguments passed to function : %A" args
    0
    
   
