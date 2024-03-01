# Git Commit Message Convention

## Message Template

```
<type>(<scope>): <subject>

<body>

<footer>
```

### Type

-   chore: Changes to our CI configuration files and scripts.
-   docs: Documentation only changes.
-   feat: A new feature.
-   fix: A bug fix.
-   perf: A code change that improves performance.
-   refactor: A code change that neither fixes a bug nor adds a feature.
-   style: Format correction, not css changes.
-   test: Adding missing tests or correcting existing tests.

### Scope

The scope could be anything specifying the microservice of the commit change.

### Subject

The subject contains a succinct description of the change:

-   max 50 characters
-   use the imperative, present tense: "change" not "changed" nor "changes"
-   don't capitalize the first letter
-   no dot (.) at the end

### Body

Just as in the subject, use the imperative, present tense: "change" not "changed" nor "changes". The body should include the motivation for the change and contrast this with previous behavior.

### Footer

The footer should contain any information about Breaking Changes and is also the place to reference GitHub issues that this commit Closes.

Breaking Changes should start with the word `BREAKING CHANGE:` with a space or two newlines. The rest of the commit message is then used for this.

### Revert

If the commit reverts a previous commit, it should begin with `revert: `, followed by the header of the reverted commit. In the body, it should say: `This reverts commit <hash>.`, where the hash is the SHA of the commit being reverted.

```
revert: feat(common): add comments option

This reverts commit 667ecc1.
```

## commit-msg hook

```bash
#!/bin/sh

commit_types="(chore|docs|feat|fix|perf|refactor|style|test)"
convetional_commit_regex="^(revert: )?${commit_types}(\([a-z \-]+\))?: .{1,50}(.|\r|\n)*$"
commit_message=$(cat "$1")

if [[ "$commit_message" =~ $convetional_commit_regex ]]; then
   echo "The commit message meets standards."
   exit 0
fi

echo "<type>(<scope>): <subject>"
echo ""
echo "<body>"
echo ""
echo "<footer>"
echo ""
echo "Valid type: ${commit_types}"

exit 1
```
