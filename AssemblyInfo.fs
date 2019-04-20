module AssemblyInfo  
  
open NUnit.Framework
   
[<assembly:LevelOfParallelism(4)>]
   
do()