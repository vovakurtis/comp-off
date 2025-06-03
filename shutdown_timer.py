import time
import argparse
import platform
import subprocess


def shutdown_after_delay(delay_seconds):
    time.sleep(delay_seconds)
    system = platform.system().lower()
    try:
        if 'windows' in system:
            subprocess.run(['shutdown', '/s', '/t', '0'], check=True)
        elif 'linux' in system or 'darwin' in system:
            # Linux or macOS
            subprocess.run(['shutdown', '-h', 'now'], check=True)
        else:
            print('Unsupported OS for automatic shutdown.')
    except Exception as e:
        print(f'Failed to execute shutdown: {e}')


def main():
    parser = argparse.ArgumentParser(description='Shutdown computer after a delay.')
    parser.add_argument('seconds', type=int, help='Delay before shutdown in seconds')
    args = parser.parse_args()
    print(f'Shutting down in {args.seconds} seconds...')
    shutdown_after_delay(args.seconds)


if __name__ == '__main__':
    main()
