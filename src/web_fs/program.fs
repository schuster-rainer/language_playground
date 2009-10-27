#light

open Web
open Utils
open System

//let google = http "http://www.google.com"
//let numLinksInGoogle = 
//    google 
//    |> getWords
//    |> List.filter (fun tag -> tag = "href")
//    |> List.length
//

//let countLinks = getWords >> List.filter (fun s -> s = "href") >> List.length
//let numLinksInGoogle = 
//       google |> countLinks       
//printfn "%d" numLinksInGoogle
//
//let sites = [ "http://www.live.com";
//              "http://www.google.com";
//              "http://search.yahoo.com" ]
//
//let show_sites_stats = fun () -> 
//    sites |> List.iter (fun site -> printfn "%s, length = %d" site (http site).Length)
//    0
//    
//Diagnostics.ProfileOut show_sites_stats

match (fetch "http://www.immowelt.de") with
    | Some(text) -> printfn "text = %s" text
    | None -> printfn "**** no web page found"
