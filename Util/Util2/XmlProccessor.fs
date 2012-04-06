module ValidationLibrary.XmlProccessor
open System.Linq
open System.IO
open System.Text.RegularExpressions

let rec prefix (i:int) (init:string) =
    if i=0 then init
    else prefix (i-1) ("0"+init)

let GetFormatString ((index:string),(isDeposit:bool)) =
    let baseIndex =if isDeposit then 10 else 8
    (prefix (baseIndex-index.Length) "")+index

 

let GetFormatStr (baseIndex:string) (index:string)  =
    (prefix (baseIndex.Length - index.Length) "")+index


let GetContinuousCustomerNo((start:string),(last:string))=
    let rec get (start:int) (last:int) (acc:string list) =
        if start=last then (string start)::acc
        elif start > last then acc
        else get (start+1) last ((string start)::acc)
    let regex=new Regex("^0+([1-9][\d]+)$")
    let regex2= new Regex("^0+([1-9][\d]+)$")
    let r1 = regex.Match(start)
    let subString m =
        match m with
        |"" -> ""
        |_ -> m.Substring(0,m.Length-1)
    match r1.Success with
    |false -> get (System.Int32.Parse(start)) (System.Int32.Parse(last)) [] |> List.fold (fun m n -> sprintf "%s%s%s" n "," m) ""|>subString
    |true ->
        let r2= regex2.Match(last)
        let r11 =System.Int32.Parse(r1.Groups.[1].Value)
        let r22 =System.Int32.Parse(r2.Groups.[1].Value)
        get r11 r22 [] |> List.map (GetFormatStr start)|> List.fold (fun m n -> sprintf "%s%s%s" n "," m) "" |> subString

let GetLikeString input =
    "N''%"+input+"%''"

let GetFormatSql ((code:string),(fax:string),(email:string),(phone:string),(addr1:string),(addr2:string),(addr3:string))=
    let mutable sql="select * from dbo.Account"
    if not (System.String.IsNullOrEmpty(code)) then sql <- sql+" and Code like "+GetLikeString code
    if not (System.String.IsNullOrEmpty(fax)) then sql <- sql+" and Fax like "+GetLikeString fax
    if not (System.String.IsNullOrEmpty(email)) then sql <- sql+" and Email like "+GetLikeString email
    if not (System.String.IsNullOrEmpty(phone)) then sql <- sql+" and phone like "+GetLikeString phone
    if not (System.String.IsNullOrEmpty(addr1)) then sql <- sql+" and addr1 like "+GetLikeString addr1
    if not (System.String.IsNullOrEmpty(addr2)) then sql <- sql+" and addr2 like "+GetLikeString addr2
    if not (System.String.IsNullOrEmpty(addr3)) then sql <- sql+" and addr3 like "+GetLikeString addr3
    let index=sql.IndexOf("and")
    if index <> -1 then sql <- sql.Substring(0,index)+"where"+sql.Substring(index+3)
    sql