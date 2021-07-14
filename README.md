# Genetic Startups
> Applications, based on Genetic Algorithms, representing possible lives of startups. The algorithm improves startup choices over generations, to achieve the most successful outcome possible; in a map where investors, product launches, team members, sad news and sales, among other options, appear.

![Showing a startup story in the web app: geneticstartups.com](https://s3-eu-west-1.amazonaws.com/genetic-startups/info/gs-web-ran-algorithm-story.png "Showing story option of best candidate in web app")

## Introduction: Genetic Algorithms

Within the field of Artificial Intelligence (AI), Genetic Algorithms (GA) are grouped in the larger class of evolutionary algorithms. And are often used as a search heuristic, to generate solutions to optimization problems.

Genetic Algorithms use techniques inspired by natural evolution, such as selection, crossover and mutation; in order to evolve a random population of possible solutions into better ones, over generations.

## The problem: Startup life evolution

Startups are surrounded with huge uncertainty and have limited resources and time to find product/market-fit and become sustainable businesses. Besides, the life of a startup is full of challenges and tough choices. As founders, we must be very careful choosing one path over another, when making decisions.

In these applications, we generate random maps that represent the space of possible choices for the life of the startup. Since finding the best path possible is key to success, we have developed a Genetic Algorithm that improves choices over generations.

There are different types of elements (_aka_ "actions") that we might encounter in the map, each with different possible values and a global score. For example:

![Types of Squares](https://s3-eu-west-1.amazonaws.com/genetic-startups/info/gs-web-cell-types.png "Description of all possible squares in the map")

There are also different probabilities for each action to appear, depending on the quarter of the map.

Learn more about the map in the dedicated information tab ["The map"](https://geneticstartups.com/info/map) at [www.geneticstartups.com](https://geneticstartups.com).

## The algorithm

### Population: defining chromosomes (encoding start cell & movements in genes)

Our evolutionary algorithm starts by defining a random population of individual potential solutions at the beginning.

Each one of those potential solutions (each individual) is defined as a binary array, represented as a set of genes (each either a "0" or a "1"). These genes are grouped in chromosomes, encoding both the start cell and the movements of each individual.

### Operators: selection, crossover and mutation

After the initial random population is created, the elements are evaluated and sorted based on their "fitness". The best candidate is selected and displayed.

Then, a new generation has to be created. Every new generation is calculated by performing 3 operations: selection, crossover and mutation. The percentages vary slightly from the original Windows-based application to the current web-based one; but the fundamentals are the same.

Learn more about our implementation in the ["Algorithm details"](https://geneticstartups.com/info/algorithm) tab of the info section at [www.geneticstartups.com](https://geneticstartups.com).

## Implementations
There will be implementations of this application using 3 different combinations of technologies / frameworks:
- *[**Current**] Web app: Rails + REST API + ReactJS* ([repository](https://github.com/romenrg/genetic-startups-web) | [website](https://geneticstartups.com))
- *[_Older_] Native Windows app:* C# + .Net + Windows Forms ([repository](https://github.com/romenrg/genetic-startups-desktop-csharp-dotnet) | [installer](https://github.com/romenrg/genetic-startups-desktop-csharp-dotnet/releases))

## Copyright and License

Copyright 2015-2021, Romen Rodríguez-Gil

Licensed under The MIT License (MIT), as described in the file LICENSE.md in the different repositories

## Contribute

Any constructive contributions (e.g. PRs or issues) are welcome. Please feel free to propose changes following
the contributing guidelines in the different repositories.