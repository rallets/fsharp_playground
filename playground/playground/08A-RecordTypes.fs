﻿namespace Playground

module RecordTypes = 

    /// This example shows how to define a new record type.  
    type ContactCard = 
        { Name     : string
          Phone    : string
          Verified : bool }
              
    /// This example shows how to instantiate a record type.
    let contact1 = 
        { Name = "Alf" 
          Phone = "(206) 555-0157" 
          Verified = false }

    /// You can also do this on the same line with ';' separators.
    let contactOnSameLine = { Name = "Alf"; Phone = "(206) 555-0157"; Verified = false }

    /// This example shows how to use "copy-and-update" on record values.
    /// It creates a new record value that is a copy of contact1, 
    /// but has different values for the 'Phone' and 'Verified' fields.
    ///
    /// To learn more, see: https://docs.microsoft.com/dotnet/fsharp/language-reference/copy-and-update-record-expressions
    let contact2 = 
        { contact1 with 
            Phone = "(206) 555-0112"
            Verified = true }

    /// This example shows how to write a function that processes a record value.
    /// It converts a 'ContactCard' object to a string.
    let showContactCard (c: ContactCard) = 
        c.Name + " Phone: " + c.Phone + (if not c.Verified then " (unverified)" else "")

    let ex1() = printfn $"Alf's Contact Card: {showContactCard contact1}"

    /// This is an example of a Record with a member.
    type ContactCardAlternate =
        { Name     : string
          Phone    : string
          Address  : string
          Verified : bool }

        /// Members can implement object-oriented members.
        member this.PrintedContactCard =
            this.Name + 
            " Phone: " + 
            this.Phone + 
            (if not this.Verified then " (unverified)" else "") + 
            " " + 
            this.Address

    let contactAlternate = 
        { Name = "Alf" 
          Phone = "(206) 555-0157" 
          Verified = false 
          Address = "111 Alf Street" }
   
    // Members are accessed via the '.' operator on an instantiated type.
    let ex2() = printfn $"Alf's alternate contact card is {contactAlternate.PrintedContactCard}"

    /// Records can also be represented as structs via the 'Struct' attribute.
    /// This is helpful in situations where the performance of structs outweighs
    /// the flexibility of reference types.
    [<Struct>]
    type ContactCardStruct = 
        { Name     : string
          Phone    : string
          Verified : bool }