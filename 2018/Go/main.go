package main

import (
	"strings"
	"sort"
	"io/ioutil"
)

func CheckLine(line string) (map[int]bool) {
	list := strings.Split(line, "")
	sort.Strings(list)
	m := make(map[int]bool)
	count := 1
	for i := 0; i < len(list)-1; i++ {
		if list[i] == list[i+1] {
			count++
		} else {
			if count > 1 {
				m[count] = true
			}
			count = 1
		}
	}
	if count > 1 {
		m[count] = true
	}
	return m
}

func Part1(input []string) int {
	m := make(map[int]int)
	for i := range(input) {
		counts := CheckLine(input[i])
		for k, _ := range counts {
			m[k]++
		}
	}
	checksum := 1
	for _, v := range(m) {
		checksum *= v
	}
	return checksum
}

func main() {
	dat, err := ioutil.ReadFile("Day02.txt")
	if err != nil {
		panic(err)
	}
	input := strings.Split(string(dat), "\n")
	print(Part1(input))
}