namespace ESTreeParser

type FenceToken =
    | FENCESTART of string
    | FENCE
    | LINE of string
