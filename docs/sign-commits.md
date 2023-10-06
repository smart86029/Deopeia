# Sign commits

## Generating a new GPG key

1.  Open Git Bash.
1.  ```shell
    gpg --full-generate-key
    ```
1.  Specify the kind of key you want.

    **(1) RSA and RSA (default)**

1.  Specify the keysize you want.

    **4096**

1.  Specify how long the key should be valid.

    Press **Enter** to specify the default selection, indicating that the key doesn't expire.

1.  Verify that your selections are correct.
1.  Enter your name.
1.  Enter your email address.
1.  Optional. Enter a comment to display in parentheses after your name.
1.  Type a secure passphrase, then enter it again to confirm it.

## Adding a GPG key

[GitHub Docs](https://docs.github.com/en/authentication/managing-commit-signature-verification/about-commit-signature-verification)

1.  ```shell
    $ gpg --list-secret-keys --keyid-format=long
    ```
1.  From the list of GPG keys, copy the long form of the GPG key ID you'd like to use. In this example, the GPG key ID is `4FFC6B025D78AA82`:

    ```shell
    /c/Users/Bill/.gnupg/pubring.kbx
    --------------------------------
    sec   rsa4096/4FFC6B025D78AA82 2023-10-05 [SC]
        97125A2115A1FC67A5F38C1A4FFC6B025D78AA82
    uid                 [ultimate] Bill Hsu <smart86029@gmail.com>
    ssb   rsa4096/BC9D70750D6788A3 2023-10-05 [E]
    ```

1.  ```shell
    gpg --armor --export 4FFC6B025D78AA82
    ```
1.  Copy your GPG key, beginning with `-----BEGIN PGP PUBLIC KEY BLOCK-----` and ending with `-----END PGP PUBLIC KEY BLOCK-----`.
1.  In the upper-right corner of any GitHub page, click your profile photo, then click **Settings**.
1.  In the **Access** section of the sidebar, click **SSH and GPG keys**.
1.  Next to the **GPG keys** header, click **New GPG key**.
1.  In the **Title** field, type a name for your GPG key.
1.  In the **Key** field, paste the GPG key you copied when you generated your GPG key.
1.  Click **Add GPG key**.
1.  To confirm the action, authenticate to your GitHub account.

## Generating a new GPG subkey

1.  ```shell
    gpg --edit-key 4FFC6B025D78AA82 addkey
    ```
1.  Specify the kind of key you want.

    **(4) RSA (sign only)**

1.  Specify the keysize you want
1.  Specify how long the key should be valid.
1.  ```shell
    gpg> save
    ```
1.  ```shell
    gpg --output signkey.gpg --export-secret-subkeys 4FFC6B025D78AA82
    ```
1.  ```shell
    gpg --delete-secret-and-public-keys FF0BC7489C37E60B!
    ```

## Import a GPG subkey

1.  Import from backup GPG file.
    ```shell
    gpg --import-options restore --import signkey.gpg
    ```
1.  ```shell
    gpg --edit-key 4FFC6B025D78AA82
    ```
1.  ```shell
    gpg> trust
    ```
1.  Specify how far you trust.

    **5 = I trust ultimate**

1.  Verify that your selections are correct.

## Git config

1.  Add `C:\Program Files\Git\usr\bin` to your `%PATH%`

1.  Set a subkey include the ! suffix.

    ```shell
    git config --global commit.gpgsign true
    ```

1.  Optionally, to configure Git to sign all commits by default.

    ```shell
    git config --global commit.gpgsign true
    ```

## Set gpg-agent cache time

To prevent type passphrase frequently.

1.  Set `%userprofile%\.gnupg\gpg-agent.conf`, the unit is seconds.

    ```
    default-cache-ttl 86400
    max-cache-ttl 604800
    ```

1.  ```shell
    gpgconf --reload gpg-agent
    ```
