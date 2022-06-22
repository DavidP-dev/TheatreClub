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
      
(*
// Return all current club members
let returnAllMembers (state : State) = state.ClubMembers

// Return all current performances
let returnAllPerformances (state : State) = state.Performances

// Takes a preferences and returns list of club members with at least on of these preferences
let getMembersByGenre (genres: Genre list ) (state: State) =
    let setOfGenres = genres |> Set.ofList
    state.ClubMembers |> List.where(fun x ->
        let preferredSet =  x.PreferredGenres |> Set.ofList
        Set.intersect setOfGenres preferredSet |> Set.count > 0)

// Takes a genre list and returns performances of these genres
let getPerformancesByGenre (genres: Genre list ) (state: State) =
    let setOfGenres = genres |> Set.ofList
    state.Performances |> List.where(fun elm ->
        let preferredSet =  elm.Genres |> Set.ofList
        Set.intersect setOfGenres preferredSet |> Set.count > 0)

// Takes a member ID and current state and returns list of reservations of this member
let getMembersReservations (clubMemberId : Guid) (state: State) =
    state.Reservations |> List.where(fun x -> x.MemberId = clubMemberId)

// Takes a current state and returns a list of non delivered reservations
let getNonDeliveredReservations (state : State) =
    state.Reservations |> List.where(fun x -> x.TicketsReceived = false)

// Takes a current state and returns a list of non paid reservations 
let getNonPaidReservations (state : State) =
    state.Reservations |> List.where(fun x -> x.IsPaid = false)
*)
