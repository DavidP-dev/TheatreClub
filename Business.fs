module Business

open System.Data
open Domain
open Database

// Takes a database connection and club member and inserts new club member to database 
let register (conn:IDbConnection) (cM:ClubMember) =
    let maybeMember = tryGetMemberByEmail conn cM.Email
    match maybeMember with
    | Some ex -> failwith $"Uzivatel s timto emailem, ID = {ex.Id} uz existuje"
    | None ->
        insertCMToDb conn cM
        
// Takes a database connection and club member and removes club member from database
let unregister (conn:IDbConnection) (cM:ClubMember) =
    let maybeMember = tryGetMemberByEmail conn cM.Email
    match maybeMember with
    | Some cM -> removeCmFromDb conn cM
    | None -> failwith "Takový uživatel v databází neexistuje."

// Takes a database connection and performance and inserts performance to database
let addPerformance (conn:IDbConnection) (perf:Performance) =
    let maybePerformance = tryGetPerformanceByTitle conn perf
    