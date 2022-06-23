module Domain

// This is database program for theatre club members management
open System
open Microsoft.FSharp.Collections

// Every performance has a specific Genre
type Genre =
    | Alternative
    | Art
    | Comedy
    | Dance  
    | Drama
    | Mainstream
    | Musical
    | Philosophy  

// Every member of club is one ClubMember record
type ClubMember =
    {
       Id : Guid
       Name : string
       Surname : string
       Email : string
       PreferredGenres : Genre list

    }
 
// Every entered theatre play is one Performance record 
type Performance =
    {
        Id : Guid
        Title : string
        Theatre : string
        DateAndTime: DateTimeOffset
        Cost : int
        Genres : Genre list
    }

// Reservation of specific game by specific members
type Reservation =
    {
        ReservationID : Guid
        MemberId : Guid
        PerformanceId : Guid
        IsPaid : bool
        TicketsReceived : bool
    }
  
  