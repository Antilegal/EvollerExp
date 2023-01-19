using UnityEngine;

public class AgentNetwork
{
    int inputsCount = 1;
    int outputsCount = 1;
    int layerSize = 1;
    int layersCount = 1;

    float[,] inputsWeightMatrix;
    float[,,] layersWeightsMatrix;
    float[,] outputsWeightMatrix;

    float[] inputs;
    float[] outputs;
    float[,] layers;

    float offset = 0f;

    public AgentNetwork()
    {
        GenerateMatrix();
    }

    public float sigma(float x) => Mathf.Clamp(1f / (1f - Mathf.Exp(-x)), -1f, 1f);

    private void GenerateMatrix()
    {
        layersWeightsMatrix = new float[layersCount, layerSize, layerSize];
        inputsWeightMatrix = new float[inputsCount, layerSize];
        outputsWeightMatrix = new float[layerSize, outputsCount];

        inputs = new float[inputsCount];
        outputs = new float[outputsCount];
        layers = new float[layersCount, layerSize];
    }

    public AgentNetwork(int countOfInputs, int sizeOfLayer, int countOfLayers, int countOfOutputs)
    {
        inputsCount = countOfInputs;
        outputsCount = countOfOutputs;
        layerSize = sizeOfLayer;
        layersCount = countOfLayers;

        GenerateMatrix();
    }

    public AgentNetwork(AgentNetwork copy)
    {
        inputsCount = copy.inputsCount;
        outputsCount = copy.outputsCount;
        layerSize = copy.layerSize;
        layersCount = copy.layersCount;

        GenerateMatrix();

        CopyMatrix(layersWeightsMatrix, copy.layersWeightsMatrix, layersCount, layerSize, layerSize);

        CopyMatrix(inputsWeightMatrix, copy.inputsWeightMatrix, inputsCount, layerSize);

        CopyMatrix(outputsWeightMatrix, copy.outputsWeightMatrix, layerSize, outputsCount);
    }

    private static void CopyMatrix(float[,] dest, float[,] src, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                dest[x, y] = src[x, y];
            }
        }
    }

    private static void CopyMatrix(float[,,] dest, float[,,] src, int width, int height, int depth)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    dest[x, y, z] = src[x, y, z];
                }
            }
        }
    }

    private static void FillMatrix(float[,,] dest, int width, int height)
    {

    }

    public void SetInput(int index, float value)
    {
        inputs[index] = value;
    }

    public float GetOutput(int index)
    {
        return outputs[index];
    }

    public AgentNetwork(AgentNetwork mather, AgentNetwork father)
    {
        inputsCount = father.inputsCount;
        outputsCount = father.outputsCount;
        layerSize = father.layerSize;
        layersCount = father.layersCount;

        GenerateMatrix();

        int x = 0;
        int y = 0;
        int z = 0;

        for (x = 0; x < layersCount; x++)
        {
            for (y = 0; y < layerSize; y++)
            {
                for (z = 0; z < layerSize; z++)
                {
                    layersWeightsMatrix[x, y, z] = (mather.layersWeightsMatrix[x, y, z] + father.layersWeightsMatrix[x, y, z]) / 2f;
                }
            }
        }

        for (x = 0; x < inputsCount; x++)
        {
            for (y = 0; y < layerSize; y++)
            {
                inputsWeightMatrix[x, y] = (mather.inputsWeightMatrix[x, y] + father.inputsWeightMatrix[x, y]) / 2f;
            }
        }

        for (x = 0; x < layerSize; x++)
        {
            for (y = 0; y < outputsCount; y++)
            {
                outputsWeightMatrix[x, y] = (mather.outputsWeightMatrix[x, y] + father.outputsWeightMatrix[x, y]) / 2f;
            }
        }
    }

    public AgentNetwork Clone()
    {
        AgentNetwork copy = new AgentNetwork(inputsCount, layerSize, layersCount, outputsCount);

        CopyMatrix(copy.layersWeightsMatrix, layersWeightsMatrix, layersCount, layerSize, layerSize);

        CopyMatrix(copy.inputsWeightMatrix, inputsWeightMatrix, inputsCount, layerSize);

        CopyMatrix(copy.outputsWeightMatrix, outputsWeightMatrix, layerSize, outputsCount);

        return copy;
    }

    public void CalculateMatrix()
    {
        int x = 0;
        int y = 0;
        int z = 0;

        for (x = 0; x < layerSize; x++)
        {
            float r = 0f;

            for (y = 0; y < inputsCount; y++)
            {
                r += (offset + inputs[y]) * inputsWeightMatrix[y, x];
            }

            layers[0, x] = sigma(r);
        }

        for (z = 1; z < layersCount; z++)
        {
            for (x = 0; x < layerSize; x++)
            {
                float r = 0f;

                for (y = 0; y < layerSize; y++)
                {
                    r += layers[z - 1, y] * layersWeightsMatrix[z - 1, y, x];
                }

                layers[z, x] = sigma(r);
            }
        }

        for (x = 0; x < outputsCount; x++)
        {
            float r = 0f;

            for (y = 0; y < layerSize; y++)
            {
                r += layers[layersCount - 1, y] * outputsWeightMatrix[y, x];
            }

            outputs[x] = sigma(r);
        }
    }

    public void Mutate(float ratio = 0.0001f)
    {
        int x = 0;
        int y = 0;
        int z = 0;

        for (x = 0; x < layersCount; x++)
        {
            for (y = 0; y < layerSize; y++)
            {
                for (z = 0; z < layerSize; z++)
                {
                    layersWeightsMatrix[x, y, z] += Random.Range(-1f, 1f) * ratio;
                }
            }
        }

        for (x = 0; x < inputsCount; x++)
        {
            for (y = 0; y < layerSize; y++)
            {
                inputsWeightMatrix[x, y] += Random.Range(-1f, 1f) * ratio;
            }
        }

        for (x = 0; x < layerSize; x++)
        {
            for (y = 0; y < outputsCount; y++)
            {
                outputsWeightMatrix[x, y] += Random.Range(-1f, 1f) * ratio;
            }
        }
    }

    public void Randomize(float ratio = 1f)
    {
        int x = 0;
        int y = 0;
        int z = 0;

        for (x = 0; x < layersCount; x++)
        {
            for (y = 0; y < layerSize; y++)
            {
                for (z = 0; z < layerSize; z++)
                {
                    layersWeightsMatrix[x, y, z] = Random.Range(-1f, 1f) * ratio;
                }
            }
        }

        for (x = 0; x < inputsCount; x++)
        {
            for (y = 0; y < layerSize; y++)
            {
                inputsWeightMatrix[x, y] = Random.Range(-1f, 1f) * ratio;
            }
        }

        for (x = 0; x < layerSize; x++)
        {
            for (y = 0; y < outputsCount; y++)
            {
                outputsWeightMatrix[x, y] = Random.Range(-1f, 1f) * ratio;
            }
        }
    }

}
