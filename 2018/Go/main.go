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

func SliceCompare(word1, word2 string) string {
	if len(word1) != len(word2) {
		return ""
	}
	for i := 0; i < len(word1); i++ {
		a := word1[:i] + word1[i+1:]
		b := word2[:i] + word2[i+1:]

		if a == b {
			return a
		}
	}
	return ""
}

func Part2(input []string) string {
	for _, word1 := range input {
		for _, word2 := range input {
			if word1 == word2 {
				continue
			}
			if res := SliceCompare(word1, word2); res != "" {
				return res
			}
		}
	}
	return ""
}

func main() {
	dat, err := ioutil.ReadFile("Day02.txt")
	if err != nil {
		panic(err)
	}
	input := strings.Split(string(dat), "\n")
	println(Part1(input))

	println(Part2(input))
}