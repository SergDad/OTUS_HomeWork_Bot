using System.Collections.Immutable;

namespace Poem1
{
    class Part1
    {
        public ImmutableList<string> Poem { get; private set; }

        public Part1()
        {
            Poem = ImmutableList<string>.Empty;
        }

        public void AddPart(ImmutableList<string> poem)
        {
            Poem = poem.AddRange(["Вот дом,",
                                  "Который построил Джек.\n"]);
            return;
        }
    }

    class Part2
    {
        public ImmutableList<string> Poem { get; private set; }

        public Part2()
        {
            Poem = ImmutableList<string>.Empty;
        }
        public void AddPart(ImmutableList<string> poem)
        {
            Poem = poem.AddRange(["А это пшеница,",
                                  "Которая в тёмном чулане хранится",
                                  "В доме,",
                                  "Который построил Джек.\n"]);
            return;
        }
    }

    public class Part3
    {
        public ImmutableList<string> Poem { get; private set; }

        public Part3()
        {
            Poem = ImmutableList<string>.Empty;
        }

        public void AddPart(ImmutableList<string> poem)
        {
            Poem = poem.AddRange(["А это весёлая птица-синица,",
                                  "Которая часто ворует пшеницу,",
                                  "Которая в тёмном чулане хранится,",
                                  "В доме,",
                                  "Который построил Джек.\n"
            ]);
            return;
        }
    }

    class Part4
    {
        public ImmutableList<string> Poem { get; private set; }

        public Part4()
        {
            Poem = ImmutableList<string>.Empty;
        }

        public void AddPart(ImmutableList<string> poem)
        {
            Poem = poem.AddRange(["Вот кот,",
                                  "Который пугает и ловит синицу,",
                                  "Которая часто ворует пшеницу,",
                                  "Которая в тёмном чулане хранится,",
                                  "В доме, который построил Джек.\n"
            ]);
            return;
        }
    }
    class Part5
    {
        public ImmutableList<string> Poem { get; private set; }

        public Part5()
        {
            Poem = ImmutableList<string>.Empty;
        }

        public void AddPart(ImmutableList<string> poem)
        {
            Poem = poem.AddRange(["Вот пес без хвоста,",
                                  "Который за шиворот треплет кота,",
                                  "который пугает и ловит синицу,",
                                  "Которая часто ворует пшеницу,",
                                  "Которая в тёмном чулане хранится,",
                                  "В доме, который построил Джек.\n"
            ]);
            return;
        }
    }

    class Part6
    {
        public ImmutableList<string> Poem { get; private set; }

        public Part6()
        {
            Poem = ImmutableList<string>.Empty;
        }

        public void AddPart(ImmutableList<string> poem)
        {
            Poem = poem.AddRange(["А это корова безрогая,",
                                  "Лягнувшая старого пса без хвоста,",
                                  "Который за шиворот треплет кота,",
                                  "который пугает и ловит синицу,",
                                  "Которая часто ворует пшеницу,",
                                  "Которая в тёмном чулане хранится,",
                                  "В доме, который построил Джек.\n"
            ]);
            return;
        }
    }

    class Part7
    {
        public ImmutableList<string> Poem { get; private set; }

        public Part7()
        {
            Poem = ImmutableList<string>.Empty;
        }

        public void AddPart(ImmutableList<string> poem)
        {
            Poem = poem.AddRange(["А это старушка, седая и строгая,",
                                  "Которая доит корову безрогую,",
                                  "Лягнувшую старого пса без хвоста,",
                                  "Который за шиворот треплет кота,",
                                  "который пугает и ловит синицу,",
                                  "Которая часто ворует пшеницу,",
                                  "Которая в тёмном чулане хранится,",
                                  "В доме, который построил Джек.\n"
            ]);
            return;
        }
    }

    class Part8
    {
        public ImmutableList<string> Poem { get; private set; }

        public Part8()
        {
            Poem = ImmutableList<string>.Empty;
        }

        public void AddPart(ImmutableList<string> poem)
        {
            Poem = poem.AddRange(["А это ленивый и толстый пастух,",
                                  "Который бранится с коровницей строгою,",
                                  "Которая доит корову безрогую,",
                                  "Лягнувшую старого пса без хвоста,",
                                  "Который за шиворот треплет кота,",
                                  "который пугает и ловит синицу,",
                                  "Которая часто ворует пшеницу,",
                                  "Которая в тёмном чулане хранится,",
                                  "В доме, который построил Джек.\n"
            ]);
            return;
        }
    }

    class Part9
    {
        public ImmutableList<string> Poem { get; private set; }

        public Part9()
        {
            Poem = ImmutableList<string>.Empty;
        }

        public void AddPart(ImmutableList<string> poem)
        {
            Poem = poem.AddRange(["Вот два петуха,",
                                  "Которые будят того пастуха,",
                                  "Который бранится с коровницей строгою,",
                                  "Которая доит корову безрогую,",
                                  "Лягнувшую старого пса без хвоста,",
                                  "Который за шиворот треплет кота,",
                                  "который пугает и ловит синицу,",
                                  "Которая часто ворует пшеницу,",
                                  "Которая в тёмном чулане хранится,",
                                  "В доме, который построил Джек.\n"
            ]);
            return;
        }
    }
}
