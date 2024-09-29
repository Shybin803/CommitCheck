using LibGit2Sharp;
using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Check if both a commit hash, source branch, and target branch were provided
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: dotnet run <commit-hash> <source-branch> <target-branch>");
            return;
        }

        string commitHash = args[0];
        string sourceBranchName = args[1];
        string targetBranchName = args[2];

        // Path to your Git repository
        string repoPath = @"/home/shybin/Documents/Learning/";

        // Open the repository
        using (var repo = new Repository(repoPath))
        {
            // Look up the commit
            var commit = repo.Lookup<Commit>(commitHash);
            if (commit == null)
            {
                Console.WriteLine($"Commit {commitHash} not found in the repository.");
                return;
            }

            // Find the source branch
            var sourceBranch = repo.Branches.FirstOrDefault(b => b.FriendlyName == sourceBranchName);
            if (sourceBranch == null)
            {
                Console.WriteLine($"Source branch '{sourceBranchName}' not found.");
                return;
            }

            // Find the target branch
            var targetBranch = repo.Branches.FirstOrDefault(b => b.FriendlyName == targetBranchName);
            if (targetBranch == null)
            {
                Console.WriteLine($"Target branch '{targetBranchName}' not found.");
                return;
            }

            // Check if the commit exists in the source branch
            if (!sourceBranch.Commits.Contains(commit))
            {
                Console.WriteLine($"Commit {commitHash} is not in the source branch '{sourceBranchName}'.");
                return;
            }

            // Check if the commit exists in the target branch
            bool isInTargetBranch = targetBranch.Commits.Contains(commit);

            if (isInTargetBranch)
            {
                Console.WriteLine($"Commit {commitHash} is found in the target branch '{targetBranchName}'. It has been moved.");
            }
            else
            {
                Console.WriteLine($"Commit {commitHash} is NOT found in the target branch '{targetBranchName}'. It has NOT been moved.");
            }
        }
    }
}
