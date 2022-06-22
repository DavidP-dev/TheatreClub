module Database

open System
open System.Data
open Domain

open Dapper.FSharp
open Dapper.FSharp.MSSQL
open Microsoft.Data.SqlClient
open Microsoft.VisualBasic

// Connection to dabase
let connstring = "data source=PICHA\\sqlexpress;initial catalog=TheatreClubDBTest;integrated security=True;TrustServerCertificate=True"

let getConnection () : IDbConnection =
    new SqlConnection(connstring)

// Database types
type MemberDB =
    {
        Id : Guid
        Name : string
        Surname : string
        Email : string
        PreferredGenres : string
    }

type PerformanceDB =
    {
        Id : Guid
        Title : string
        Theatre : string
        DateAndTime: DateTimeOffset
        Cost : int
        Genres : string
    }

type ReservationDB =
        {
        ReservationID : Guid
        MemberId : Guid
        PerformanceId : Guid
        IsPaid : bool
        TicketsReceived : bool
        }

// Modules for transferring data from database layer to domain layer and opposite 
module MembersDb =
    let parseGenre (gn :string) : Genre =
        match gn with
        | "Alternative" -> Alternative
        | "Art" -> Art
        | "Comedy" -> Comedy
        | "Dance" -> Dance
        | "Drama" -> Drama
        | "Mainstream" -> Mainstream
        | "Musical" -> Musical
        | "Philosophy" -> Philosophy
        | _ -> failwith $"There is no genre {gn}!"
        
    let genreToString (gn: Genre) : string =
        match gn with
        | Alternative -> "Alternative"
        | Art -> "Art"
        | Comedy -> "Comedy"
        | Dance -> "Dance"
        | Drama -> "Drama"
        | Mainstream -> "Mainstream"
        | Musical -> "Musical"
        | Philosophy -> "Philosophy"
            
    let toDomain (db:MemberDB) : ClubMember = {
       Id = db.Id
       Name = db.Name
       Surname = db.Surname
       Email = db.Email
       PreferredGenres = db.PreferredGenres.Split(",") |> Array.map parseGenre |> List.ofArray}
    
    let toDatabase (dm:ClubMember) : MemberDB = {
        Id = dm.Id
        Name = dm.Name
        Surname = dm.Surname
        Email = dm.Email
        PreferredGenres = dm.PreferredGenres |> List.map genreToString |> (fun x -> String.Join(",", x))} 

module PerformancesDB =
    let toDomain (db:PerformanceDB) : Performance = {
        Title = db.Title
        Theatre = db.Theatre
        DateAndTime = db.DateAndTime
        Cost = db.Cost
        Genres =  db.Genres.Split(",") |> Array.map MembersDb.parseGenre |> List.ofArray
        Id = db.Id}
    
    let toDatabase (dm:Performance) : PerformanceDB = {
        Id = dm.Id
        Title = dm.Title
        Theatre = dm.Theatre
        DateAndTime = dm.DateAndTime
        Cost = dm.Cost
        Genres = dm.Genres |> List.map MembersDb.genreToString |> (fun x -> String.Join(",", x))}

module ReservationDB =
    let toDomain (db:ReservationDB) : Reservation = {
        ReservationID = db.ReservationID 
        MemberId = db.MemberId
        PerformanceId = db.PerformanceId
        IsPaid = db.IsPaid
        TicketsReceived = db.TicketsReceived
    }
    let toDatabase (dm:Reservation) : ReservationDB = {
        ReservationID = dm.ReservationID
        MemberId = dm.MemberId
        PerformanceId = dm.PerformanceId
        IsPaid = dm.IsPaid
        TicketsReceived = dm.IsPaid
    }

// names of tables

let membersTable = table'<MemberDB> "ClubMembers"

let performancesTable = table'<PerformanceDB> "Performances"

let ReservationsTable = table'<ReservationDB> "Reservations"


 

// Checks existence of club member by searching for his email in database
let tryGetMemberByEmail (conn : IDbConnection) (email : string) =
    //let dbMember = MembersDb.toDatabase cM
    let vysl =
        select {
            for m in membersTable do
            where (m.Email = email)
        }
        |> conn.SelectAsync<MemberDB>
  
  
    let v = vysl.Result
    v
    |> Seq.tryHead
    |> Option.map (fun x -> MembersDb.toDomain x)

// adds Member to database

let insertCMToDb (conn:IDbConnection) (cM:ClubMember) =
    let dbMember = MembersDb.toDatabase cM
    insert {
        into membersTable
        value dbMember
    }
    |> conn.InsertAsync


// removes Member from Database
let removeCmFromDb (conn:IDbConnection) (cM:ClubMember) =
    delete {
        for m in membersTable do
        where (m.Id = cM.Id )}
    |> conn.DeleteAsync


// function add Performance
let addPerformanceToDb (conn:IDbConnection) (perf:Performance) =
    let dbPerformance = PerformancesDB.toDatabase perf
    insert {
        into performancesTable
        value dbPerformance
    }
    |> conn.InsertAsync
    
// function remove Performance
let removePerformanceFromDb (conn:IDbConnection) (perf:Performance) =
    delete {
        for p in performancesTable do
        where (p.Id = perf.Id )}
    |> conn.DeleteAsync

// function add Reservation
let addReservationToDb (conn:IDbConnection) (res:Reservation) =
    let dbReservation = ReservationDB.toDatabase res
    insert {
        into ReservationsTable
        value dbReservation
    }
    |> conn.InsertAsync

// function remove Reservation
let removeReservationFromDb (conn:IDbConnection) (res:Reservation) =
    delete {
        for r in ReservationsTable do
        where (r.ReservationID = res.ReservationID )}
    |> conn.DeleteAsync

// Returns all club members in database
// Returns club members by preffered genres
// Returns performances by Genres
// Returns all reservations
// Returns all undelivered reservations
// Returns all unpaid reservation
