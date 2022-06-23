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
    | Some x -> removeCmFromDb conn cM
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
    | Some (maybeReservation) -> failwith $"Rezervace ID {res.ReservationID} je už databázi." 
    | None -> addReservationToDb conn res
    
// Checks reservation existence and removes reservation from database
let removeReservation (conn:IDbConnection) (res:Reservation) =
    let maybeReservation = tryGetReservationByIds conn res
    match maybeReservation with
    | Some (maybeReservation) -> failwith "Taková rezervace v dabázi neexistuje." 
    | None -> removeReservationFromDb conn res

// Returns all Club members from database
let getAllClubMembers (conn:IDbConnection) =
    let clubMemberList = returnAllClubMembersFromDb conn
    printfn "Zde je aktuální seznam členů klubu: %A" clubMemberList 

// Returns all performances
let getAllPerformances (conn:IDbConnection) =
    let performancesList = returnAllPerformancesFromDb conn
    printfn "Zde je aktuální seznam her v dabázi: %A" performancesList
    
// Returns all reservations
let getAllReservations (conn:IDbConnection) =
    let reservationsList = returnAllReservationsFromDb conn
    printfn "Zde je aktuální seznam rezervací: %A" reservationsList
    
// Returns all undelivered reservations
let getAllUndeliveredReservations (conn:IDbConnection) =
    let undeliveredReservationsList = returnAllUndeliveredReservations conn
    printfn "Zde je aktuální seznam nedoručených vstupenek: %A" undeliveredReservationsList
    
// Returns all unpaid reservations
let getAllUnpaidreservations (conn:IDbConnection) =
    let unPaidReservationsList = returnAllUnpaidReservations conn
    printfn "Zde je aktuální seznam nezaplacených objednávek %A" unPaidReservationsList

