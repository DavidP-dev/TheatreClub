module Domain

// This is database program for theatre club members management
open System
open Microsoft.FSharp.Collections

// Every performance has a specific Genre
type Genre =
    | Alternative
    | Comedy
    | Drama
    | Mainstream
    | Philosophy
    

// Every member of club is one ClubMember record
type ClubMember =
    {
       Name : string
       Surname : string
       Email : string
       PreferredGenres : Genre list
       Id : Guid
    }

 
// Every entered theatre play is one Performance record 
type Performance =
    {
        Title : string
        DateAndTime: DateTimeOffset
        Cost : int
        Genres : Genre list
        Id : Guid
    }


// Reservation of specific game by specific members
type Reservation =
    {
        MemberId : Guid
        PerformanceId : Guid
        IsPaid : bool
        TicketsReceived : bool
    }


(*
Current state of database.
It contains list of members of Theatre club,
lists of performances
and map of attendance
*)
type State =
    {
        ClubMembers : ClubMember list
        Performances : Performance list
        Reservations : Reservation list
    }
    

// Check ClubMember existence and add ClubMember to ClubMembers list    
let register (cMember : ClubMember) (state : State) =
    let userExists =
        state.ClubMembers |> List.exists( fun x -> x.Name = cMember.Name && x.Surname = cMember.Surname && x.Email = cMember.Email)
    if not userExists then
        
        {
            state with ClubMembers =  cMember :: state.ClubMembers
        }
    else failwith "Club member is already in database!"
    
    
// Check ClubMember existence and remove ClubMember from ClubMembers list
let unregister (cMember : ClubMember) (state : State) =
    let userExists =
        state.ClubMembers |> List.exists( fun x -> x.Name = cMember.Name && x.Surname = cMember.Surname && x.Email = cMember.Email)
    if userExists then
        
        {
            state with ClubMembers = state.ClubMembers |> List.where(fun x -> x <> cMember)
        }
    else failwith "Club member is not present in the database!"


// Check performance existence and add Performance to Performances list
let add_performance (performance : Performance) (state : State) =
    let performanceExists =
        state.Performances |> List.exists(fun x -> x.Title = performance.Title)
    if not performanceExists then
        {
            state with Performances = performance :: state.Performances
        }
    else failwith "User exists"


// Check Performance existence and remove Performance from Performances list
let removePerformance (performance : Performance) (state : State) =
    let userExists =
        state.Performances |> List.exists( fun x -> x.Title = performance.Title)
    if userExists then
        
        {
            state with Performances = state.Performances |> List.where(fun x -> x <> performance)
        }
    else failwith "Club member is not present in the database!"

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


// Current state of app
let initialState = {
    ClubMembers = []
    Performances = []
    Reservations = []
}
