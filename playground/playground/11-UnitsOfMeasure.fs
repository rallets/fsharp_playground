﻿namespace Playground

/// Units of measure are a way to annotate primitive numeric types in a type-safe way.
/// You can then perform type-safe arithmetic on these values.
///
/// To learn more, see: https://docs.microsoft.com/dotnet/fsharp/language-reference/units-of-measure
module UnitsOfMeasure = 

    /// First, open a collection of common unit names
    open Microsoft.FSharp.Data.UnitSystems.SI.UnitNames

    /// Define a unitized constant
    let sampleValue1 = 1600.0<meter>          

    /// Next, define a new unit type
    [<Measure>]
    type mile =
        /// Conversion factor mile to meter.
        static member asMeter = 1609.34<meter/mile>

    /// Define a unitized constant
    let sampleValue2 = 500.0<mile>          

    /// Compute metric-system constant
    let sampleValue3 = sampleValue2 * mile.asMeter   

    // Values using Units of Measure can be used just like the primitive numeric type for things like printing.
    let ex1() = printfn $"After a %f{sampleValue1} meters race I would walk %f{sampleValue2} miles which would be %f{sampleValue3} meters"
