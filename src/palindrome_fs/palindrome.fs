#light

open Utils

let reverse number =  
    new string( number.ToString().ToCharArray() 
                |> Array.rev )
    |> System.Int32.Parse
  

let is_palindromic number = 
    number = reverse number    
    
let products range =  
    seq { for a in range do
            for b in range do                
                yield a*b
    }
    
let palindromic_max = Seq.filter (fun x -> is_palindromic x ) >> Seq.max

let  max_three_digict_product_palindrome = fun () -> 
    products {100..999} 
    |> palindromic_max

Diagnostics.ProfileOut max_three_digict_product_palindrome
//Diagnostics.Out profiledFunc

//let max_palindrome_with_seq_filter = fun () -> 
//    products {100..999}
//    |> Seq.filter (fun x -> is_palindromic x )     
//    |> Seq.max
//
//Diagnostics.ProfileOut max_palindrome_with_seq_filter

//let max_palindrome_with_inline_filter = 
//    let palindromic_products min max =  
//        seq { for a in range do
//                for b in range do
//                    let product = a*b
//                    if is_palindromic product then
//                        yield product
//        }
//    let palindromes = palindromic_products {1000..9999}
//    Seq.max palindromes
//
//Diagnostics.profile (fun () -> max_palindrome_with_inline_filter)

