module Business

open System.Data
open Domain
open Database

// Checks club member existence and adds club member to database 
let register (conn:IDbConnection) (cM:ClubMember) =
    let maybeMember = tryGetMemberByEmail conn cM.Email
    match maybeMember with
    | Some ex -> failwith $"Uzivatel s timto emailem, ID = {ex.Id} uz existuje"
    | None ->
        insertCMToDb conn cM
        
// Checks club member existence and removes club member from database
let unregister (conn:IDbConnection) (cM:ClubMember) =
    let maybeMember = tryGetMemberByEmail conn cM.Email
    match maybeMember with
    | Some(maybeMember) -> removeCmFromDb conn cM
    | None -> failwith "Takový uživatel v databází neexistuje."

// Checks performance existence and inserts performance to database
let addPerformance (conn:IDbConnection) (perf:Performance) =
    let maybePerformance = tryGetPerformanceByTitleAndDate conn perf
    match maybePerformance with
    | Some ex -> failwith $"Divadlení představení {ex.Title} již v databázi existuje."
    | None -> addPerformanceToDb conn perf

// Checks performance existence and and removes performance from database
let removePerformance (conn:IDbConnection) (perf:Performance) =
    let maybePerformance = tryGetPerformanceByTitleAndDate conn perf
    match maybePerformance with
    | Some (maybePerformance) -> removePerformanceFromDb conn perf
    | None -> failwith $"Divadlení představení {perf.Title} není v databázi."
    
// Checks reservation existence and adds reservation to database
let addReservation (conn:IDbConnection) (res:Reservation) =
    let maybeReservation = tryGetReservationByIds conn res
    match maybeReservation with
    | Some ex -> failwith $"Rezervace ID {res.ReservationID} je už databázi." 
    | None -> addReservationToDb conn res
    
// Checks reservation existence and removes reservation from database
let removeReservation (conn:IDbConnection) (res:Reservation) =
    let maybeReservation = tryGetReservationByIds conn res
    match maybeReservation with
    | Some ex -> failwith $"Rezervace ID {res.ReservationID} je už databázi." 
    | None -> addReservationToDb conn res