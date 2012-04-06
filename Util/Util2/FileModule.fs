module FileModule
open System.IO
let rec GetDirectorySize dir =
    Seq.append (dir |>Directory.GetFiles)
               (dir |> Directory.GetDirectories |> Seq.map GetDirectorySize |> Seq.concat)

let GetSize dir =
    dir |> GetDirectorySize |> Seq.map (fun m -> 
        let file = File.Open(m,FileMode.Append)
        file.Length
        )
    |> Seq.sum
   
   
    
        
     
        
