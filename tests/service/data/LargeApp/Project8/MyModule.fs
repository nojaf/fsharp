namespace Project8

module MyModule =
    let sayHello name = sprintf "Hello %s!" name
    let (|A|B|) v = if (v % 2) = 0 then A else B