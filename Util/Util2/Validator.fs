
#light
module VilidationLibrary.Validator
open System.Text.RegularExpressions
let IsPhone input=
    let rex=new Regex(@"\d{7}\d*$")
    rex.IsMatch(input)

let IsFax input =
    let rex=new Regex(@"^[+]{0,1}(\d){1,5}?(\d){1,4}?(\d){1,10}([-]\d{1,4})?$")
    rex.IsMatch(input)

let IsEmail input =
    let rex= new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")
    rex.IsMatch(input)

