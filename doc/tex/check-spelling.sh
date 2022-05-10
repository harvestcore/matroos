#/bin/bash

SpellCheck() {
    filename=$1.spellcheck
    cat $1 | aspell --lang=en --mode=tex list | aspell --lang=es --mode=tex --personal=./dictionary.txt list | sort > $filename
    output=$(cat $filename | wc -l)
}

errors=false

for file in $(find . -name "*.tex"); do
    SpellCheck $file
    
    if [ $output -gt 0 ]; then
        printf "\n\nThere are $output errors in $file. See:\n"
        cat $filename
        errors=true
    fi
done

if [ $errors = true ]; then
    exit 1
else
    exit 0
fi
