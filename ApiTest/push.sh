#!/bin/bash

NOW=$(date +"%m-%d-%Y")
echo "Hello Ozgur $(date)"

git add .

git commit -m "changes on $(date)"

git push
