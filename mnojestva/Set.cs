using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnojestva
{
    class Set 
    {
        public string[] elements;
        public double[] affiliations;

        public Set(string[] elements, double[] affiliations)
        {
            this.elements = elements;
            this.affiliations = affiliations;
        }

        public Set Union(Set B)
        {
            string[] elementsC = elements.Union(B.elements).ToArray();
            double[] affiliationsC = new double[elementsC.Length];

            int indexA = 0;
            int indexB = 0;

            for (int i = 0; i < elementsC.Length; i++)
            {
                if (elements.Contains(elementsC[i]) && B.elements.Contains(elementsC[i]))
                {
                    for (int j = 0; j < elements.Length; j++)
                        if (elements[j] == elementsC[i])
                            indexA = j;

                    for (int j = 0; j < B.elements.Length; j++)
                        if (B.elements[j] == elementsC[i])
                            indexB = j;

                    affiliationsC[i] = Math.Max(affiliations[indexA], B.affiliations[indexB]);
                    indexA = 0;
                    indexB = 0;
                }

                if (elements.Contains(elementsC[i]))
                {
                    for (int j = 0; j < elements.Length; j++)
                        if (elements[j] == elementsC[i])
                            indexA = j;

                    affiliationsC[i] = affiliations[indexA];
                    indexA = 0;
                }

                if (B.elements.Contains(elementsC[i]))
                {
                    for (int j = 0; j < B.elements.Length; j++)
                        if (B.elements[j] == elementsC[i])
                            indexB = j;

                    affiliationsC[i] = B.affiliations[indexB];
                    indexB = 0;
                }
            }
            return new Set(elementsC, affiliationsC);
        }

        public Set Intersect(Set B)
        {
            string[] elementsC = elements.Intersect(B.elements).ToArray();
            double[] affiliationsC = new double[elementsC.Length];

            int indexA = 0;
            int indexB = 0;

            for (int i = 0; i < elementsC.Length; i++)
            {
                for (int j = 0; j < elements.Length; j++)
                    if (elements[j] == elementsC[i])
                        indexA = j;

                for (int j = 0; j < B.elements.Length; j++)
                    if (B.elements[j] == elementsC[i])
                        indexB = j;

                affiliationsC[i] = Math.Max(affiliations[indexA], B.affiliations[indexB]);
                indexA = 0;
                indexB = 0;
                
            }
            return new Set(elementsC, affiliationsC);
        }

        public Set Difference(Set B)
        {
            List<string> elementsC = new List<string>();
            List<double> affiliationsС = new List<double>();

            for (int i = 0; i < elements.Length; i++)
            {
                if (B.elements.Contains(elements[i]))
                {
                    for (int j = 0; j < B.elements.Length; j++)
                    {
                        if (this.elements[i] == B.elements[j])
                        {
                            double affiliation = affiliations[i] - B.affiliations[j];
                            if (affiliation > 0)
                            {
                                elementsC.Add(elements[i]);
                                affiliationsС.Add(affiliation);
                            }
                        }
                    }
                }
                else
                {
                    elementsC.Add(elements[i]);
                    affiliationsС.Add(affiliations[i]);
                }
            }
            return new Set(elementsC.ToArray(), affiliationsС.ToArray());
        }

        public Set Symmetricdiff(Set B)
        {
            Set C = Union(B);
            Set D = Intersect(B);
            Set E = C.Difference(D);
            return E;
        }

        public Set Extension()
        {
            string[] elementsU = new string[] {"A","B","C","D","E","F","G",
                "H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"};
            double[] affiliationsU = new double[elementsU.Length];
            for (int i = 0; i < elementsU.Length; i++)
                affiliationsU[i] = 1;
            Set Universal = new Set(elementsU, affiliationsU);
            Set NotA = Universal.Difference(this);
            return NotA;
        }
    }
}
