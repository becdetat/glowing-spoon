# Work180 code test

## Executing

The brains of the test is written as a library assembly (the `Elevator` project, the logic is in the `Brains` class). The unit tests are in the `Elevator.Tests` project, using NUnit, BDDfy and Shouldly. Execute the `Elevator.Tests` project in your favourite test runner.

## Explanation

I started out with a naive implementation of finding the next passenger's request, just picking the next from the queue. This become more involved until I was writing a rather complex series of `OrderBy` calls to get the next request. This stopped working efficiently on the last test case, where I switched to finding the most efficient ordering of requests using a brute-force approach over all permutations of the queued requests. I figure that there will never be enough requests in the queue for the efficiency of the algorithm to be a practical issue.

The permutation implementation was shamelessly copied verbatim from [Stack Overflow](https://stackoverflow.com/a/15150493).

