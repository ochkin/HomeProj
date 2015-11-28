module Util

let numberOfDigits n =
    if n < 0I then
      invalidArg "n" "Negative"
    else
      n.ToString().Length