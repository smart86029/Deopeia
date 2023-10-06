# Git Commit Message Convention

```
<type>(<jira>): <subject>

<body>

<footer>
```

### Type

- feat: A new feature.
- fix: A bug fix.
- docs: Documentation only changes.
- style: Format correction, not css changes.
- refactor: A code change that neither fixes a bug nor adds a feature.
- perf: A code change that improves performance.
- test: Adding missing tests or correcting existing tests.
- chore: Changes to our CI configuration files and scripts.

### Scope

The scope could be anything specifying the microservice of the commit change.

### Subject

The subject contains a succinct description of the change:

- use the imperative, present tense: "change" not "changed" nor "changes"
- don't capitalize the first letter
- no dot (.) at the end

### Body

Just as in the subject, use the imperative, present tense: "change" not "changed" nor "changes". The body should include the motivation for the change and contrast this with previous behavior.

### Footer

The footer should contain any information about Breaking Changes and is also the place to reference GitHub issues that this commit Closes.

Breaking Changes should start with the word `BREAKING CHANGE:` with a space or two newlines. The rest of the commit message is then used for this.

### Revert

If the commit reverts a previous commit, it should begin with `revert: `, followed by the header of the reverted commit. In the body, it should say: `This reverts commit <hash>.`, where the hash is the SHA of the commit being reverted.

```
revert: feat(common): add 'comments' option

This reverts commit 667ecc1654a317a13331b17617d973392f415f02.
```
