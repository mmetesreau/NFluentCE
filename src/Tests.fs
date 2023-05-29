module Tests

open System
open Xunit

module CheckCE =
    open NFluent
    
    type CheckDateTime(value: DateTime) =
        [<CustomOperation("isBefore")>]
        member x.IsBefore(check: ICheck<DateTime>, value: DateTime) =
            check.IsBefore(value).And
            
        member x.Yield(_) = Check.That(value)
        
        member x.Run(check: ICheck<DateTime>) = ()

    type StringCheck(value: string) =
        [<CustomOperation("isEqualTo")>]
        member x.IsEqualTo(check: ICheck<string>, value: string) =
            check.IsEqualTo(value).And
            
        [<CustomOperation("isNotEqualTo")>]
        member x.IsNotEqualTo(check: ICheck<string>, value: string) =
            check.Not.IsEqualTo(value).And
        
        member x.Yield(_) = Check.That(value)
        
        member x.Run(check: ICheck<string>) = ()
        
    type ObjectCheck(value: Object) =
        [<CustomOperation("isEqualTo")>]
        member x.IsEqualTo(check: ICheck<Object>, value: Object) =
            check.IsEqualTo(value).And
            
        [<CustomOperation("isNotEqualTo")>]
        member x.IsNotEqualTo(check: ICheck<Object>, value: Object) =
            check.Not.IsEqualTo(value).And
        
        member x.Yield(_) = Check.That(value)
        
        member x.Run(check: ICheck<Object>) = ()
        
    type CheckCE() =
        static member checkThat(value: DateTime) = CheckDateTime value
        static member checkThat(value: string) = StringCheck value
        static member checkThat(value: Object) = ObjectCheck value

open type CheckCE.CheckCE

[<Fact>]
let ``Let's try`` () =
    
    checkThat {| Toto = 1 |} {
        isEqualTo {| Tata = "xxx" |}
    }

    checkThat "olala" {
        isEqualTo "olala"
        isNotEqualTo "olala"
    }
    
    checkThat DateTime.Now {
        isBefore (DateTime.Now.AddDays(-1))
    }

open NFluent

[<Fact>]
let ``Play`` () =
    Check.That({|Toto = "a"|}).IsEqualTo({|Toto = "addd"|})
    
