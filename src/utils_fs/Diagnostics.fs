#light

namespace Utils

open System;
open System.Diagnostics;

module public Diagnostics = 
    let public Profile action = 
        let stopwatch = new Stopwatch()
        stopwatch.Start()

        let result = action()

        stopwatch.Stop()

        let stackTrace = new StackTrace(false)
        let stackFrame = stackTrace.GetFrame(1);
        
        (result, stopwatch, stackFrame)

    let public Out (result, (stopwatch:Stopwatch), (stackFrame:StackFrame)) = 
        Console.WriteLine("msec: {0} in {1}",
                    stopwatch.Elapsed.TotalMilliseconds,
                    stackFrame.GetMethod());

        Console.WriteLine("Die Antwort auf alle Fragen: {0}", box result)
        
    let public ProfileOut action = 
        Profile action |> Out
        