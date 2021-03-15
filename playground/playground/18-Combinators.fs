namespace Playground

/// Combinators are the basis of a whole branch of logic (naturally called “combinatory logic”) that was invented many years before computers and programming languages. 
/// Combinatory logic has had a very large influence on functional programming.
/// To read more about combinators and combinatory logic, I recommend the book “To Mock a Mockingbird” by Raymond Smullyan. 
/// In it, he describes many other combinators and whimsically gives them names of birds. 
///
/// To learn more, see: https://fsharpforfunandprofit.com/posts/defining-functions/
module Combinators =

    // Here are some examples of some standard combinators and their bird names:

    let I x = x                // identity function, or the Idiot bird
    let K x y = x              // the Kestrel => Fluent interfaces (is a common pattern in fluent interfaces where you do something but then return the original object)
    let M x = x >> x           // the Mockingbird
    let T x y = y x            // the Thrush (this looks familiar!) => Pipe
    let Q x y z = y (x z)      // the Queer bird (also familiar!) => Forward composition
    let S x y z = x z (y z)    // The Starling
    // and the infamous...
    let rec Y f x = f (Y f) x  // Y-combinator, or Sage bird (is famously used to make functions recursive)
