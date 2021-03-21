namespace ParserAPI

module Models = 

    type CarModel = {Company: string; 
                     Model: string; 
                     Mileage: int; 
                     EnginePower: int; 
                     EngineVolume: double; 
                     Year: int; 
                     Transmission: bool; 
                     Link: string;
                     Price: int}