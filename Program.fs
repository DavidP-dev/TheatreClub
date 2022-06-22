// For more information see https://aka.ms/fsharp-console-apps
module Program

open System
open Domain
open Business

(*

// Check ClubMember existence and add ClubMember to ClubMembers list
// Místo statu připojení k databázi   
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
let addPerformance (performance : Performance) (state : State) =
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
            state with Performances = state.Performances |> List.where(fun x -> x.Title <> performance.Title)
        }
    else failwith "Club member is not present in the database!"

// Check Reservation existence and add reservation to Reservation list
let addReservation (reservation : Reservation) (state : State) =
    let reservationExists =
        state.Reservations |> List.exists(fun x -> (x.MemberId = reservation.MemberId) && (x.PerformanceId = reservation.PerformanceId))
    if not reservationExists then
        {
        state with Reservations = reservation :: state.Reservations
        }
    else failwith "Reservation is already exists in the database!"
      
// Check Reservation existence and remove reservation from Reservation list
let removeReservation (reservation : Reservation) (state : State) =
    let reservationExists =
        state.Reservations |> List.exists(fun x -> (x.MemberId = reservation.MemberId) && (x.PerformanceId = reservation.PerformanceId))
    
    if not reservationExists then
        {
        state with Reservations = state.Reservations |> List.where(fun x -> x.ReservationID <> reservation.ReservationID )
        }
    else failwith "Reservation is not in the database!"
*)

// Lower is function test

let david = {
    Name = "David"
    Surname = "Pícha"
    Email = "picha@sskpk.cz"
    PreferredGenres = [Art; Philosophy; Comedy]
    Id = Guid.NewGuid()
}


let conn = Database.getConnection ()

register conn david