namespace ParserAPI

open System

module HelperTryParse = 
    let TryParseIntOption(value: string) = 
        match (Int32.TryParse value) with 
        | true, res -> Some res
        | _, _ -> None

    let TryParseDoubleOption(value: string) = 
        match (Double.TryParse value) with 
        | true, res -> Some res
        | _, _ -> None