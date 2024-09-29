#!/bin/bash

# Ensure commit hash, source branch, and target branch are provided
if [ $# -lt 3 ]; then
  echo "Usage: $0 <commit-hash> <source-branch> <target-branch>"
  exit 1
fi

# Assign arguments to variables
COMMIT_HASH=$1
SOURCE_BRANCH=$2
TARGET_BRANCH=$3

# Check if we're inside a Git repository
if ! git rev-parse --is-inside-work-tree > /dev/null 2>&1; then
  echo "Not a valid Git repository."
  exit 1
fi

# Check if the commit exists in the source branch
if git branch --contains $COMMIT_HASH | grep -q "$SOURCE_BRANCH"; then
  echo "Commit $COMMIT_HASH exists in the source branch '$SOURCE_BRANCH'."
else
  echo "Commit $COMMIT_HASH does not exist in the source branch '$SOURCE_BRANCH'."
  exit 1
fi

# Check if the commit exists in the target branch
if git branch --contains $COMMIT_HASH | grep -q "$TARGET_BRANCH"; then
  echo "Commit $COMMIT_HASH has been moved to the target branch '$TARGET_BRANCH'."
else
  echo "Commit $COMMIT_HASH has NOT been moved to the target branch '$TARGET_BRANCH'."
fi

