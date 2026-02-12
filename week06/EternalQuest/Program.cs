using System;


namespace EternalQuest
{
    // Abstract base class
    public abstract class Goal
    {
        protected string _shortName;
        protected string _description;
        protected int _points;

        public Goal(string name, string description, int points)
        {
            _shortName = name;
            _description = description;
            _points = points;
        }

        public string ShortName => _shortName;

        public abstract void RecordEvent();

        public abstract bool IsComplete();

        // Returns how many points are earned IN THIS event (includes possible bonus)
        public abstract int GetPointsThisEvent();

        public virtual string GetDetailsString()
        {
            return $"{GetDisplayStatus()} {_shortName} ({_description})";
        }

        protected virtual string GetDisplayStatus()
        {
            return IsComplete() ? "[X]" : "[ ]";
        }

        public abstract string GetStringRepresentation();
    }


    public class SimpleGoal : Goal
    {
        private bool _isComplete;

        public SimpleGoal(string name, string description, int points)
            : base(name, description, points)
        {
            _isComplete = false;
        }

        public override void RecordEvent()
        {
            if (!_isComplete)
            {
                _isComplete = true;
            }
        }

        public override bool IsComplete() => _isComplete;

        public override int GetPointsThisEvent()
        {
            return _isComplete ? 0 : _points;
        }

        public override string GetStringRepresentation()
        {
            return $"SimpleGoal:{_shortName},{_description},{_points},{(_isComplete ? "1" : "0")}";
        }

        // Factory method to load
        public static SimpleGoal FromString(string data)
        {
            var parts = data.Split(',');
            var goal = new SimpleGoal(parts[0], parts[1], int.Parse(parts[2]));
            goal._isComplete = parts[3] == "1";
            return goal;
        }
    }


    public class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points)
            : base(name, description, points)
        {
        }

        public override void RecordEvent()
        {
            // State does not change
        }

        public override bool IsComplete() => false;

        public override int GetPointsThisEvent() => _points;

        public override string GetDetailsString()
        {
            return $"[∞] {_shortName} ({_description}) — Eternal";
        }

        public override string GetStringRepresentation()
        {
            return $"EternalGoal:{_shortName},{_description},{_points}";
        }

        public static EternalGoal FromString(string data)
        {
            var parts = data.Split(',');
            return new EternalGoal(parts[0], parts[1], int.Parse(parts[2]));
        }
    }


    public class ChecklistGoal : Goal
    {
        private int _amountCompleted;
        private readonly int _target;
        private readonly int _bonus;

        public ChecklistGoal(string name, string description, int points, int target, int bonus)
            : base(name, description, points)
        {
            _amountCompleted = 0;
            _target = target;
            _bonus = bonus;
        }

        public override void RecordEvent()
        {
            if (_amountCompleted < _target)
            {
                _amountCompleted++;
            }
        }

        public override bool IsComplete() => _amountCompleted >= _target;

        public override int GetPointsThisEvent()
        {
            int basePoints = _points;

            // If this event completes the goal → add the bonus
            if (_amountCompleted == _target - 1 && !IsComplete())
            {
                return basePoints + _bonus;
            }

            return basePoints;
        }

        public override string GetDetailsString()
        {
            string status = IsComplete() ? "[X]" : $"[{_amountCompleted}/{_target}]";
            string bonusText = IsComplete() ? "" : $" — Bonus {_bonus} pts when finished";
            return $"{status} {_shortName} ({_description}){bonusText}";
        }

        protected override string GetDisplayStatus()
        {
            return $"[{_amountCompleted}/{_target}]";
        }

        public override string GetStringRepresentation()
        {
            return $"ChecklistGoal:{_shortName},{_description},{_points},{_target},{_bonus},{_amountCompleted}";
        }

        public static ChecklistGoal FromString(string data)
        {
            var parts = data.Split(',');
            var g = new ChecklistGoal(
                parts[0], parts[1], int.Parse(parts[2]),
                int.Parse(parts[3]), int.Parse(parts[4])
            );
            g._amountCompleted = int.Parse(parts[5]);
            return g;
        }
    }


    public class GoalManager
    {
        private List<Goal> _goals = new List<Goal>();
        private int _score;

        public void Start()
        {
            Console.WriteLine("=== Eternal Quest ===\n");
            LoadGoals();

            while (true)
            {
                Console.Clear();
                DisplayPlayerInfo();
                Console.WriteLine("\nMenu Options:");
                Console.WriteLine("  1. List goals");
                Console.WriteLine("  2. Create new goal");
                Console.WriteLine("  3. Record event");
                Console.WriteLine("  4. Save and exit");
                Console.WriteLine("  5. Exit without saving");
                Console.Write("\nSelect an option: ");

                string opt = Console.ReadLine()?.Trim();

                switch (opt)
                {
                    case "1": ListGoalDetails(); break;
                    case "2": CreateGoal(); break;
                    case "3": RecordEvent(); break;
                    case "4": SaveGoals(); return;
                    case "5": Console.WriteLine("Goodbye!"); return;
                    default: Console.WriteLine("Invalid option."); Console.ReadKey(); break;
                }
            }
        }

        private void DisplayPlayerInfo()
        {
            Console.WriteLine($"Score: {_score} points");
        }

        private void ListGoalDetails()
        {
            Console.WriteLine("\n--- Goals ---");
            if (_goals.Count == 0)
            {
                Console.WriteLine("No goals yet.");
            }
            else
            {
                for (int i = 0; i < _goals.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
                }
            }
            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }

        private void CreateGoal()
        {
            Console.WriteLine("\nTypes of goals:");
            Console.WriteLine("  1. Simple goal (one-time)");
            Console.WriteLine("  2. Eternal goal (repeating)");
            Console.WriteLine("  3. Checklist goal (multiple times + bonus)");
            Console.Write("Choose type (1-3): ");
            string type = Console.ReadLine()?.Trim();

            Console.Write("Name: ");
            string name = Console.ReadLine()?.Trim();

            Console.Write("Description: ");
            string desc = Console.ReadLine()?.Trim();

            Console.Write("Points per event: ");
            if (!int.TryParse(Console.ReadLine(), out int points) || points <= 0)
            {
                Console.WriteLine("Invalid points. Cancelling.");
                Console.ReadKey();
                return;
            }

            Goal newGoal = null;

            if (type == "1")
            {
                newGoal = new SimpleGoal(name, desc, points);
            }
            else if (type == "2")
            {
                newGoal = new EternalGoal(name, desc, points);
            }
            else if (type == "3")
            {
                Console.Write("How many times to complete for bonus? ");
                if (!int.TryParse(Console.ReadLine(), out int target) || target < 1)
                {
                    Console.WriteLine("Invalid number.");
                    Console.ReadKey(); return;
                }

                Console.Write("Bonus points when finished: ");
                if (!int.TryParse(Console.ReadLine(), out int bonus) || bonus < 0)
                {
                    Console.WriteLine("Invalid bonus.");
                    Console.ReadKey(); return;
                }

                newGoal = new ChecklistGoal(name, desc, points, target, bonus);
            }

            if (newGoal != null)
            {
                _goals.Add(newGoal);
                Console.WriteLine("\nGoal created successfully!");
            }
            else
            {
                Console.WriteLine("Invalid type.");
            }

            Console.ReadKey();
        }

        private void RecordEvent()
        {
            if (_goals.Count == 0)
            {
                Console.WriteLine("No goals created yet.");
                Console.ReadKey();
                return;
            }

            ListGoalDetails(); // show numbered list

            Console.Write("\nWhich goal number did you accomplish? ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 1 && idx <= _goals.Count)
            {
                Goal goal = _goals[idx - 1];
                int earned = goal.GetPointsThisEvent();

                if (earned > 0)
                {
                    goal.RecordEvent();
                    _score += earned;
                    Console.WriteLine($"\nGreat! You earned {earned} points.");
                    Console.WriteLine($"New total: {_score} points");
                }
                else
                {
                    Console.WriteLine("This goal is already complete (no more points).");
                }
            }
            else
            {
                Console.WriteLine("Invalid number.");
            }

            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }

        private void SaveGoals()
        {
            string filename = "eternal-quest.txt";

            try
            {
                using (var writer = new StreamWriter(filename))
                {
                    writer.WriteLine(_score);
                    foreach (var goal in _goals)
                    {
                        writer.WriteLine(goal.GetStringRepresentation());
                    }
                }
                Console.WriteLine($"\nSaved to {filename}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving: {ex.Message}");
            }

            Console.ReadKey();
        }

        private void LoadGoals()
        {
            string filename = "eternal-quest.txt";
            if (!File.Exists(filename)) return;

            try
            {
                var lines = File.ReadAllLines(filename);
                if (lines.Length == 0) return;

                _score = int.Parse(lines[0]);
                _goals.Clear();

                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string type = line.Split(':')[0];
                    string data = line.Substring(type.Length + 1);

                    if (type == "SimpleGoal")
                        _goals.Add(SimpleGoal.FromString(data));
                    else if (type == "EternalGoal")
                        _goals.Add(EternalGoal.FromString(data));
                    else if (type == "ChecklistGoal")
                        _goals.Add(ChecklistGoal.FromString(data));
                }

                Console.WriteLine($"Loaded {_goals.Count} goals and {_score} points.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading: {ex.Message}");
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var manager = new GoalManager();
            manager.Start();
        }
    }
}