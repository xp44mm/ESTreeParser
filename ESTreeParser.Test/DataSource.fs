namespace ESTreeParser

type DataSource() =        
    static member files =
        [|
            "5"
            "2015"
            "2016"
            "2017"
            "2018"
            "2019"
            "2020"
            "2021"
            "2022"
            |]

    static member filesForMemberData =
        DataSource.files
        |> Seq.map(box>>Array.singleton)
