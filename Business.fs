module Business

open System.Data
open Domain
open Database

// Takes database connection and inserts new club member to database 
let register (conn:IDbConnection) (cM:ClubMember) =
    let maybeMember = tryGetMemberByEmail conn cM.Email
    match maybeMember with
    | Some ex -> failwith $"Uzivatel s timto emailem, ID = {ex.Id} uz existuje"
    | None ->
        insert conn cM
        
    
    