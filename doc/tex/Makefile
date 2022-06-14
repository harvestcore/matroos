DOC := proyecto.tex

all: doc

doc: $(DOC:.tex=.pdf)

all: proyecto.pdf

%.pdf: %.tex
	pdflatex $< && bibtex $* && pdflatex $< && pdflatex $<

spellrights:
	chmod +x ./check-spelling.sh 

spell:
	./check-spelling.sh

clean:
	rm -rf *.pdf *.out *.lot *.log *.lof *.aux *.toc *.spellcheck **/*.spellcheck *.toc *.rws *.bbl *.blg *.gz